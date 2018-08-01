using Microsoft.AspNet.Identity;
using MyBudget.Models;
using MyBudget.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBudget.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private ApplicationDbContext _context;

        public TransactionsController()
        {
            _context = new ApplicationDbContext();
        }


        //Главное окно        
        public ActionResult MyBudget(string id)
        {
            string UserGuid = User.Identity.GetUserId();
            DateTime dt;

            if (String.IsNullOrEmpty(id))//по умолчанию текущий месяц
            { 
                dt = DateTime.Now;
                id = dt.ToString("MMyyyy");
            }
            else
                dt = DateTime.ParseExact(id, "MMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);

            var s = dt.ToString("Y", new CultureInfo("ru-RU"));

            var viewModel = new MyListViewModel
            {                
                MyGoals = _context.Goals.Where(m => m.UserId == UserGuid).ToList(),
                ListDate = dt.ToString("Y", new CultureInfo("ru-RU"))
            };
            return View(viewModel);
        }

        public ActionResult TransactionForm(bool? id)
        {
            string UserGuid = User.Identity.GetUserId();            
            var categories = _context.Users.Find(UserGuid).Categories.Where(c => c.IsSpendingCategory==id);

            if (id == true)
                ViewBag.Head = "Добавить расход";
            else
                ViewBag.Head = "Добавить доход";

            var viewModel = new TransactionFormViewModel
            {
                Categories = categories,
                IsSpending = id
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Save(Transaction transaction)
        {
            if (transaction.Id == 0)
            {
                transaction.UserId = User.Identity.GetUserId();
                _context.Transactions.Add(transaction);
            }
            else
            {
                var transactionInDb = _context.Transactions.Single(t => t.Id == transaction.Id);
                transactionInDb.Name = transaction.Name;
                transactionInDb.Amount = transaction.Amount;
                transactionInDb.CategoryId = transaction.CategoryId;
                transactionInDb.Description = transaction.Description;
                transactionInDb.IsSpending = transaction.IsSpending;
                transactionInDb.TransDate = transaction.TransDate;
                transactionInDb.IsSpending = transaction.IsSpending;
                transactionInDb.IsPlaned = transaction.IsPlaned;
                transactionInDb.UserId = transaction.UserId;
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

            return RedirectToAction("MyBudget", "Transactions");
        }

        public ActionResult Edit(int id)
        {            
            string UserGuid = User.Identity.GetUserId();           
            var transaction = _context.Transactions.SingleOrDefault(c => c.Id == id);
            if (transaction == null)
                return HttpNotFound();

            var categories = _context.Users.Find(UserGuid).Categories.Where(c=>c.IsSpendingCategory==transaction.IsSpending);

            var viewModel = new TransactionFormViewModel
            {
                Transaction = transaction,
                Categories = categories,
                IsSpending = transaction.IsSpending
            };

            return View("TransactionForm",viewModel);
        }

        public ActionResult Delete(int id)
        {
            var transaction = _context.Transactions.SingleOrDefault(t => t.Id == id);            
            if (transaction == null)
                return HttpNotFound();

            _context.Transactions.Remove(transaction);

            _context.SaveChanges();
            return RedirectToAction("MyBudget", "Transactions");
        }

        public ActionResult ChangeIsPlaned(int id)
        {
            var transaction = _context.Transactions.SingleOrDefault(t => t.Id == id);
            if (transaction == null)
                return HttpNotFound();

            transaction.IsPlaned = !transaction.IsPlaned;            
            _context.SaveChanges();
            return RedirectToAction("MyBudget", "Transactions");
        }

    }
}