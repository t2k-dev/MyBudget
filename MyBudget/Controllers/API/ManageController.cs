using Microsoft.AspNet.Identity;
using MyBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyBudget.Controllers.API
{
    public class ManageController : ApiController
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
    }
}
