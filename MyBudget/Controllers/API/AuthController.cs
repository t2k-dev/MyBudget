using MyBudget.Models;
using MyBudget.Models.ApiDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyBudget.Controllers.API
{
    public class AuthController : ApiController
    {
        private ApplicationDbContext _context;

        public AuthController()
        {
            _context = new ApplicationDbContext();
        }

        


        ///<summary>
        /// Получаем настройки пользователя
        ///</summary>

        [HttpGet]
        [Route("api/getconfig")]
        public IHttpActionResult GetConfig(string id)
        {
            try
            {
                var user = _context.Users.SingleOrDefault(u => u.Id == id);
                if (user == null)
                    return NotFound();

                return Ok( new { user.CarryoverRests, user.DefCurrency, user.UpdateDate, user.UseTemplates });
            }
            catch (Exception)
            {

            }
            return BadRequest();
        }               


    }
}
