using Microsoft.AspNet.Identity;
using MyBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBudget.Controllers
{
    public class GraphController : Controller
    {
        private ApplicationDbContext _context;

        public GraphController()
        {
            _context = new ApplicationDbContext();
        }


        public ActionResult Pie()
        {
            string UserGuid = User.Identity.GetUserId();
            ViewBag.DefCurrency = _context.Users.SingleOrDefault(u=>u.Id==UserGuid).DefCurrency;
            return View();
        }
    }
}