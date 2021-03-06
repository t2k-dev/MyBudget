﻿using Microsoft.AspNet.Identity;
using MyBudget.BusinessLogic;
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
                Categories = _context.Users.Find(userId).Categories.Where(c => (!c.IsSystem)).OrderBy(c => c.Name).ToList()
            };
            return View(viewmodel);
        }

        // GET: Category
        public ActionResult CategoryForm(bool? id)
        {
            var viewModel = new CategoryFormViewModel
            {

            };
            return View(viewModel);
        }

        public ActionResult AddSpendingCategory()
        {
            var category = new Category()
            {
                IsSpendingCategory = true,
                CreatedBy = User.Identity.GetUserId()
            };

            var viewmodel = new CategoryFormViewModel
            {
                Category = category
            };

            return View("CategoryForm", viewmodel);
        }

        public ActionResult AddIncomeCategory()
        {
            var category = new Category()
            {
                IsSpendingCategory = false,
                CreatedBy = User.Identity.GetUserId()
            };

            var viewmodel = new CategoryFormViewModel
            {
                Category = category
            };

            return View("CategoryForm", viewmodel);
        }


        [HttpPost]
        public ActionResult Save(Category category)
        {
            if (!ModelState.IsValid)
            {
                var viewmodel = new CategoryFormViewModel
                {
                    Category = category
                };

                return View("CategoryForm", viewmodel);
            }


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
            return RedirectToAction("UserCategories", "Category");
        }

        public ActionResult DeleteFromMyCategories(int id)
        {
            var editUser = _context.Users.Find(User.Identity.GetUserId());

            var categoryService = new CategoryService(id, editUser.Id);
            categoryService.DeleteCategory();

            return RedirectToAction("UserCategories");
        }

    }
}