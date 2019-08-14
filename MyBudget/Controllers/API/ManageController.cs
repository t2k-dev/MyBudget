using Microsoft.AspNet.Identity;
using MyBudget.FiltersApi;
using MyBudget.Models;
using MyBudget.Models.ApiDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyBudget.Controllers.API
{
    public class ManageController : BaseApiController
    {
        private ApplicationDbContext _context;

        public ManageController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPut]
        [Route("api/manage/setdefaultcurrency/{curChar}")]
        public void SetDefaultCurrency(string curChar)
        {
            string UserGuid = User.Identity.GetUserId();            
            _context.Users.SingleOrDefault(u => u.Id == UserGuid).DefCurrency = curChar;
            _context.SaveChanges();

        }

        
        [HttpPost]
        [ValidateModel]
        [Route("api/manage/changepassword")]
        public async Task<IHttpActionResult> ChangePassword([FromBody]ChangePasswordDTO model)
        {
            try
            {
                if (model == null)
                    return BadRequest("You've sent an empty model");

                var result = await this.AppUserManager.ChangePasswordAsync(model.UserId, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                    return BadRequest();
                       
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
