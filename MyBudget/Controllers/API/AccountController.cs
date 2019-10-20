using System;

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MyBudget.BusinessLogic;
using MyBudget.FiltersApi;
using MyBudget.Models;
using MyBudget.Models.ApiDTOs;
using MyBudget.Infrastructure;
using static MyBudget.Models.ApiDTOs.NewLoginResponseDTO;
using System.Collections.Generic;
using MyBudget.Models.ApiDTOs.Transactions;
using MyBudget.Models.ApiDTOs.Account;

namespace MyBudget.Controllers.API
{
    public class AccountController : BaseApiController
    {
        private ApplicationSignInManager _signInManager;        
        private ApplicationDbContext _context;
        private PasswordHasher _hasher;

        public AccountController()
        {
            _context = new ApplicationDbContext();            
            _hasher = new PasswordHasher();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IPasswordHasher hasher)
        {            
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }



        /// <summary>
        /// Register new User
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [Route("api/register")]
        public async Task<IHttpActionResult> Register(RegisterRequestDTO model)
        {
            try
            {
                if (model == null)
                    return BadRequest("You've sent an empty model");

                var user = new ApplicationUser { UserName = model.Username, Email = model.Email };
                var result = await AppUserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    CategoryService categoryService = new CategoryService();
                    categoryService.AddDefaultCategories(user.Id);
                }
                else
                {
                    string _errors = "";
                    foreach (var error in result.Errors)
                        _errors = $"{_errors}{error};";
                    throw new Exception(_errors);
                }
                return Created("", user.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ValidateModel]
        [Route("api/updateSettings/{userId}")]
        public async Task<IHttpActionResult> UpdateSettings(UserSettingsUpdateRequestDTO model, string userId)
        {
            try
            {
                if (model == null)
                    return BadRequest("You've sent an empty model");

                var userInDbo = _context.Users.Single(u => u.Id == userId);
                userInDbo.DefCurrency = model.DefCurrency ?? userInDbo.DefCurrency;
                userInDbo.CarryoverRests = model.CarryoverRests ?? userInDbo.CarryoverRests;
                userInDbo.UseTemplates = model.UseTemplates ?? userInDbo.UseTemplates;

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ValidateModel]
        [Route("api/login")]
        public IHttpActionResult Login([FromBody]LoginRequestDTO model)
        {
            try
            {
                if (model == null)
                    return BadRequest("You've sent an empty model");

                int statusCode = 200;

                // Проверка пользователя
                var user = _context.Users.SingleOrDefault(u => u.UserName == model.usr);
                if (user == null)
                {
                    statusCode = 401;
                    LoginResponseDTO erroResult = new LoginResponseDTO
                    {
                        Status = 401
                    };
                    return Ok(erroResult);
                }
                // Проверка пароля
                if (_hasher.VerifyHashedPassword(user.PasswordHash, model.pass) == PasswordVerificationResult.Success)
                {
                    // Ежемесячные операции
                    var monthlyOpsService = new MonthlyOpsService(user.Id);
                    monthlyOpsService.ExecuteMonthlyOps();

                    LoginResponseDTO result = new LoginResponseDTO
                    {
                        Status = statusCode,
                        UserId = user.Id,
                        CarryOverRests = user.CarryoverRests,
                        DefCurrency = user.DefCurrency,
                        UpdateDate = user.UpdateDate,
                        UseTemplates = user.UseTemplates
                    };

                    return Ok(result);
                }
                else
                {
                    statusCode = 401;
                    LoginResponseDTO erroResult = new LoginResponseDTO
                    {
                        Status = 401
                    };
                    return Ok(erroResult);
                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ValidateModel]
        [Route("api/newLogin")]
        public IHttpActionResult NewLogin([FromBody]LoginRequestDTO model)
        {
            try
            {
                if (model == null)
                    return BadRequest("You've sent an empty model");

                int statusCode = 200;

                // Проверка пользователя
                var user = _context.Users.SingleOrDefault(u => u.UserName == model.usr);
                if (user == null)
                {
                    statusCode = 401;
                    LoginResponseDTO erroResult = new LoginResponseDTO
                    {
                        Status = 401
                    };
                    return Ok(erroResult);
                }
                // Проверка пароля
                if (_hasher.VerifyHashedPassword(user.PasswordHash, model.pass) == PasswordVerificationResult.Success)
                {
                    // Ежемесячные операции
                    var monthlyOpsService = new MonthlyOpsService(user.Id);
                    monthlyOpsService.ExecuteMonthlyOps();

                    var userSettings = new UserSettingsModel
                    {
                        CarryOverRests = user.CarryoverRests,
                        DefCurrency = user.DefCurrency,
                        UpdateDate = user.UpdateDate,
                        UseTemplates = user.UseTemplates,
                        UserId = user.Id                        
                    };

                    var categories = _context.Users.Find(user.Id).Categories.ToList();
                    List<CategoryDTO> ReturnCatList = new List<CategoryDTO>();
                    foreach (var item in categories)
                    {
                        ReturnCatList.Add(new CategoryDTO
                        {
                            Id = item.Id,
                            Name = item.Name,
                            IsSpendingCategory = item.IsSpendingCategory,
                            IsSystem = item.IsSystem,
                            CreatedBy = item.CreatedBy,
                            Icon = item.Icon
                        });

                    }
                    var transactions = _context.Transactions.Where(t => t.UserId == user.Id && t.TransDate.Year == DateTime.Now.Year && t.TransDate.Month == DateTime.Now.Month).ToList();
                    List<TransactionListItem> transactionsList = new List<TransactionListItem>();
                    foreach (var transaction in transactions)
                    {
                        TransactionListItem listItem = new TransactionListItem()
                        {
                            Id = transaction.Id,
                            Amount = transaction.Amount,
                            CategoryId = transaction.CategoryId,
                            IsPlaned = transaction.IsPlaned,
                            IsSpending = transaction.IsSpending,
                            Name = transaction.Name,
                            TransDate = transaction.TransDate,
                            UserId = transaction.UserId
                        };
                        transactionsList.Add(listItem);
                    }
                    

                    NewLoginResponseDTO result = new NewLoginResponseDTO
                    {
                        Status = statusCode,                        
                        UserSettings = userSettings,
                        Categories = ReturnCatList,
                        Transactions = transactionsList
                    };

                    return Ok(result);
                }
                else
                {
                    statusCode = 401;
                    LoginResponseDTO erroResult = new LoginResponseDTO
                    {
                        Status = 401
                    };
                    return Ok(erroResult);
                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [ValidateModel]
        [Route("api/forgotPassword")]
        public async Task<IHttpActionResult> ForgotPassword(ForgotPasswordRequestDTO model)
        {
            try
            {
                if (model == null)
                    return BadRequest("You've sent an empty model");

                var user = new ApplicationUser();

                if (!string.IsNullOrEmpty(model.Email))
                {
                    user = AppUserManager.FindByEmail(model.Email);
                }
                else if (!string.IsNullOrEmpty(model.Username))
                {
                    user = AppUserManager.FindByName(model.Username);
                }

                if (user == null)
                    return NotFound();
                
                string code = await AppUserManager.GeneratePasswordResetTokenAsync(user.Id);
                string newPassord = StringUtils.GeneratePassword();

                var resetResul = await AppUserManager.ResetPasswordAsync(user.Id, code, newPassord);                

                await AppUserManager.SendEmailAsync(user.Id, "Сброс пароля для MuBudget", $"Ваш временный пароль: {newPassord}");


                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
