using Microsoft.AspNet.Identity;
using MyBudget.Models;
using MyBudget.ViewModels;
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
            var userId = User.Identity.GetUserId();
            var viewModel = new GraphPieViewModel()
            {
                UserId = userId,
                DefCurrency = _context.Users.SingleOrDefault(u => u.Id == userId).DefCurrency
            };
            return View(viewModel);
        }
    }
}