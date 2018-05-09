using Microsoft.AspNet.Identity;
using MyBudget.Models;
using MyBudget.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBudget.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext _context;

        public CategoryController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Category
        public ActionResult CategoryForm(bool? id)
        {
            var viewModel = new CategoryFormViewModel
            {                
                IsSpending = id
            };
            return View(viewModel);            
        }


        [HttpPost]
        public ActionResult Save(Category category)
        {
            if (category.Id == 0)
            {
                var UserGuid = User.Identity.GetUserId();
                category.CreatedBy = UserGuid;
                category.Users.Add(_context.Users.Find(UserGuid));
                _context.Categories.Add(category);
            }
            else
            {
                /*Для редактирования*/
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Manage");
        }
    }
}