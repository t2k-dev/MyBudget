using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using MyBudget.BusinessLogic;
using MyBudget.FiltersApi;
using MyBudget.Models;
using MyBudget.Models.ApiDTOs;


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
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context) );        
            _hasher = new PasswordHasher();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IPasswordHasher hasher)
        {
            UserManager = userManager;            
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

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
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
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Заполняем базовые категории
                    using (_context)
                    {
                        var userInDb = _context.Users.Single(t => t.UserName == user.UserName);
                        foreach (Category ct in _context.Categories.Where(c => (c.CreatedBy == null) || (c.CreatedBy.Contains("SYS"))).ToList())
                            userInDb.Categories.Add(ct);
                        _context.SaveChanges();
                    }
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
        [Route("api/token")]
        public async Task<IHttpActionResult> CreateToken([FromBody] CredentialModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                if (_hasher.VerifyHashedPassword(user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JUSTDOITJUSTDOITJUSTDOIT"));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        issuer: "http//mybudget.com",
                        audience: "",
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        signingCredentials: creds
                        );
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }                                    
            }
            return BadRequest();
        }
    }
}
