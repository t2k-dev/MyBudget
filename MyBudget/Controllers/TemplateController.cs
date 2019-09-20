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
    public class TemplateController : Controller
    {
        private ApplicationDbContext _context;

        public TemplateController()
        {
            _context = new ApplicationDbContext();
        }

        
        public ActionResult TemplateList(string id)
        {
            string UserGuid = User.Identity.GetUserId();
            var viewModel = new TemplateListViewModel
            {
                MyTemplates = _context.Templates.Include("Category").Where(t => t.UserId == UserGuid).OrderBy(t => t.Day).ToList(),
                DefCurrency = _context.Users.Single(u => u.Id == UserGuid).DefCurrency
            };

            return View(viewModel);
        }        

        public ActionResult AddTemplate(bool IsSpending)
        {
            int NoCategoryId;
            if (IsSpending)
            {
                ViewBag.Head = "Добавить расход";
                NoCategoryId = Category.SpendingCategoryDefault;
            }
            else
            {
                ViewBag.Head = "Добавить доход";
                NoCategoryId = Category.IncomeCategoryDefault;
            }

            string UserGuid = User.Identity.GetUserId();

            var categories = _context.Users.Find(UserGuid).Categories
                .Where(c => c.IsSpendingCategory == IsSpending
                    && (!c.IsSystem || (c.Id == Category.SpendingCategoryDefault || c.Id == Category.IncomeCategoryDefault)))
                .ToList();            
            var defCategory = categories.First(c => c.Id == NoCategoryId);


            categories.Remove(defCategory);
            categories = categories.OrderBy(c => c.Name).ToList();
            categories.Insert(0, defCategory);

            var defCurrency = _context.Users.Single(u => u.Id == UserGuid).DefCurrency;

            var template = new Template();
            template.IsSpending = IsSpending;
            template.UserId = UserGuid;

            // Список дней
            List<int> days = new List<int>();
            for (int i = 1; i <= 28; i++)
            {
                days.Add(i);
            }

            var viewModel = new TemplateFormViewModel
            {
                Template = template,
                Categories = categories,
                DefCurrency = defCurrency,
                Days = new SelectList(days)
            };

            return View("TemplateForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            string UserGuid = User.Identity.GetUserId();
            var defCurrency = _context.Users.Single(u => u.Id == UserGuid).DefCurrency;

            var template = _context.Templates.SingleOrDefault(t => t.Id == id);
            if (template == null)
                return HttpNotFound();            

            int DefCatId = template.IsSpending ? Category.SpendingCategoryDefault : Category.IncomeCategoryDefault;

            var categories = _context.Users.Find(UserGuid).Categories
                .Where(c => c.IsSpendingCategory == template.IsSpending 
                        && (!c.IsSystem || (c.Id == Category.SpendingCategoryDefault || c.Id == Category.IncomeCategoryDefault)))
                .ToList();
            var defCategory = categories.First(c => c.Id == DefCatId);
            categories.Remove(defCategory);
            categories = categories.OrderBy(c => c.Name).ToList();
            categories.Insert(0, defCategory);

            // Список дней
            List<int> days = new List<int>();
            for (int i = 1; i <= 28; i++)
            {
                days.Add(i);
            }

            var viewModel = new TemplateFormViewModel
            {
                Template = template,
                Categories = categories,                
                DefCurrency = defCurrency,
                Days = new SelectList(days)
            };

            return View("TemplateForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Template template)
        {
            if (!ModelState.IsValid)
            {
                string UserGuid = User.Identity.GetUserId();
                var categories = _context.Users.Find(UserGuid).Categories.Where(c => c.IsSpendingCategory == template.IsSpending).OrderBy(c => c.Name);
                var defCurrency = _context.Users.Single(u => u.Id == UserGuid).DefCurrency;

                var viewModel = new TemplateFormViewModel()
                {
                    Template = template,
                    Categories = categories,
                    DefCurrency = defCurrency
                };

                return View("TemplateForm", viewModel);
            }

            if (template.Id == 0)
            {
                _context.Templates.Add(template);
            }
            else
            {
                var templateInDb = _context.Templates.Single(t => t.Id == template.Id);
                templateInDb.Name = template.Name;
                templateInDb.Amount = template.Amount;
                templateInDb.CategoryId = template.CategoryId;
                templateInDb.IsSpending = template.IsSpending;
                templateInDb.Day = template.Day;
                templateInDb.UserId = template.UserId;
            }

            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var errors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in errors.ValidationErrors)
                    {
                        // get the error message 
                        string errorMessage = validationError.ErrorMessage;
                    }
                }
            }

            return RedirectToAction("TemplateList", "Template");
        }

        public ActionResult Delete(int id)
        {
            var template = _context.Templates.SingleOrDefault(t => t.Id == id);
            if (template == null)
                return HttpNotFound();

            _context.Templates.Remove(template);

            _context.SaveChanges();
            return RedirectToAction("TemplateList", "Template");
        }
    }
}
