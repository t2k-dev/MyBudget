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

        public ActionResult UserCategories()
        {
            var userId = User.Identity.GetUserId();           
            var viewmodel = new UserCategoriesViewModel
            {
                Categories = _context.Users.Find(userId).Categories.Where(c => (c.CreatedBy == null) || (c.CreatedBy== userId)).ToList()
            };
            return View(viewmodel);
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

        public ActionResult DeleteFromMyCategories(int id)
        {            
            var editUser = _context.Users.Find(User.Identity.GetUserId());
            var editCat = _context.Categories.First(c => c.Id == id);
            editUser.Categories.Remove(editCat);
            _context.SaveChanges();
            return RedirectToAction("UserCategories");
        }

    }
}