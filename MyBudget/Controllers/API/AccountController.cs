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

namespace MyBudget.Controllers.API
{
    public class AccountController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _context;
        private PasswordHasher _hasher;

        public AccountController()
        {
            _context = new ApplicationDbContext();
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
            _hasher = new PasswordHasher();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IPasswordHasher hasher)
        {
            _userManager = userManager;
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
                var result = await _userManager.CreateAsync(user, model.Password);
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
                    //var monthlyOpsService = new MonthlyOpsService(user.Id);
                    //monthlyOpsService.ExecuteMonthlyOps();

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

    }
}
