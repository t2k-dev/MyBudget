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
            var  user = _context.Users.Single(u => u.Id == UserGuid);



            DateTime dt;            
            if (String.IsNullOrEmpty(id))             
                dt = DateTime.Now; //по умолчанию текущий месяц            
            else
                dt = DateTime.ParseExact(id, "MMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);

            //Ежемесячные платежи
            if (UpdateDateExpired(user.UpdateDate))
            {
                if (user.CarryoverRests)                
                    AddRestTransaction(UserGuid);                                    

                user.UpdateDate = DateTime.Now;
                _context.SaveChanges();
            }



            var viewModel = new MyListViewModel
            {                
                MyGoals = _context.Goals.Where(m => m.UserId == UserGuid).ToList(),
                ListDate = dt.ToString("Y", new CultureInfo("ru-RU")),
                DefCurrency = user.DefCurrency
            };
            return View(viewModel);
        }

        public ActionResult TransactionForm(bool? id)
        {
            string UserGuid = User.Identity.GetUserId();            
            var categories = _context.Users.Find(UserGuid).Categories.Where(c => c.IsSpendingCategory==id);

            var defCurrency = _context.Users.Single(u => u.Id == UserGuid).DefCurrency;

            if (id == true)
                ViewBag.Head = "Добавить расход";
            else
                ViewBag.Head = "Добавить доход";

            var viewModel = new TransactionFormViewModel
            {
                Categories = categories,
                IsSpending = id,
                DefCurrency = defCurrency
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Save(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                string UserGuid = User.Identity.GetUserId();
                var categories = _context.Users.Find(UserGuid).Categories.Where(c => c.IsSpendingCategory == transaction.IsSpending);

                var viewModel = new TransactionFormViewModel()
                {
                    Transaction = transaction,
                    Categories = categories
                };

                return RedirectToAction("TransactionForm", "Transactions", new { id = "True" });
                    //View("TransactionForm", viewModel,);
            }

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
            var defCurrency = _context.Users.Single(u => u.Id == UserGuid).DefCurrency;

            var transaction = _context.Transactions.SingleOrDefault(c => c.Id == id);
            if (transaction == null)
                return HttpNotFound();

            var categories = _context.Users.Find(UserGuid).Categories.Where(c=>c.IsSpendingCategory==transaction.IsSpending);

            var viewModel = new TransactionFormViewModel
            {
                Transaction = transaction,
                Categories = categories,
                IsSpending = transaction.IsSpending,
                DefCurrency = defCurrency
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

        
        private bool UpdateDateExpired(DateTime? UpdateDate)
        {
            if (UpdateDate == null)
                return true;

            var mDate = (DateTime)UpdateDate;
            if (mDate.Year < DateTime.Now.Year)
                return true;
            else if (mDate.Month < DateTime.Now.Month)
                return true;

            return false;
        }

        private void AddRestTransaction(string UserGuid)
        {            
            string sDate = DateTime.Now.AddMonths(-1).ToString("MMyyyy");
            var transactions = _context.Transactions.Where(m => (m.UserId == UserGuid) && (m.IsPlaned==false)).ToList().Where(m => m.TransDate.ToString("MMyyyy") == sDate).ToList();
            double sum = transactions.Where(x => x.IsSpending == false).Sum(x => x.Amount) - transactions.Where(x => x.IsSpending == true).Sum(x => x.Amount);

            if (sum <= 0)
                return;

            var transaction = new Transaction
            {
                Amount = sum,
                IsPlaned = false,
                IsSpending = false,
                Name = "Остаток за прошлый месяц",
                UserId = UserGuid,
                TransDate = DateTime.Now,
                CategoryId = _context.Categories.SingleOrDefault(c=> c.CreatedBy == "SYS_2").Id
            };

            _context.Transactions.Add(transaction);
        }

    }
}