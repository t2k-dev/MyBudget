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
    public class TransactionsController : Controller
    {
        private ApplicationDbContext _context;

        public TransactionsController()
        {
            _context = new ApplicationDbContext();
        }


        //Главное окно
        public ActionResult MyBudget()
        {
            string UserGuid = User.Identity.GetUserId();
            var viewModel = new MyListViewModel
            {                
                MyTransactions = _context.Transactions.Where(m => m.UserId == UserGuid).ToList(),                
                MyGoals = _context.Goals.ToList()
                
            };
            return View(viewModel);
        }

        public ActionResult TransactionForm()
        {
            var categories = _context.Categories.ToList();
            var viewModel = new TransactionFormViewModel
            {
                Categories = categories
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
            var transaction = _context.Transactions.SingleOrDefault(c => c.Id == id);
            if (transaction == null)
                return HttpNotFound();
            var viewModel = new TransactionFormViewModel
            {
                Transaction = transaction,
                Categories = _context.Categories.ToList()
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
    }
}