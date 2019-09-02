using ClosedXML.Excel;
using Microsoft.AspNet.Identity;
using MyBudget.BusinessLogic;
using MyBudget.Models;
using MyBudget.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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


        /// <summary>
        /// Главное окно
        /// </summary>
        /// <param name="id">MMyyyy</param>        
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
            var monthlyOpsService = new MonthlyOpsService(UserGuid);
            monthlyOpsService.ExecuteMonthlyOps();

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
            
            int DefCatId = id == true ? Category.SpendingCategoryDefault : Category.IncomeCategoryDefault;
            
            var categories = _context.Users.Find(UserGuid).Categories.Where(c => c.IsSpendingCategory == id).ToList();
            var defCategory = categories.First(c => c.Id == DefCatId);
            categories.Remove(defCategory);
            categories.OrderBy(c => c.Name);
            categories.Insert(0, defCategory);


            var defCurrency = _context.Users.Single(u => u.Id == UserGuid).DefCurrency;

            if (id == true)
                ViewBag.Head = "Добавить расход";
            else
                ViewBag.Head = "Добавить доход";

            var viewModel = new TransactionFormViewModel
            {
                Transaction = new Transaction(),
                Categories = categories,
                //IsSpending = id,
                DefCurrency = defCurrency
            };
            return View(viewModel);
        }

        public ActionResult AddTransaction(bool isSpending)
        {
            int NoCategoryID;
            if (isSpending)
            {
                ViewBag.Head = "Добавить расход";
                NoCategoryID = Category.SpendingCategoryDefault;
            }
            else
            {
                ViewBag.Head = "Добавить доход";
                NoCategoryID = Category.IncomeCategoryDefault;
            }
            
            string UserGuid = User.Identity.GetUserId();

            var categories = _context.Users.Find(UserGuid).Categories.Where(c => c.IsSpendingCategory == isSpending).ToList();
            var defCategory = categories.First(c => c.Id == NoCategoryID);
            categories.Remove(defCategory);
            categories = categories.OrderBy(c => c.Name).ToList();
            categories.Insert(0, defCategory);

            var defCurrency = _context.Users.Single(u => u.Id == UserGuid).DefCurrency;

            var transaction = new Transaction();
            transaction.IsSpending = isSpending;
            transaction.TransDate = DateTime.Now;
            transaction.UserId = UserGuid;

            var viewModel = new TransactionFormViewModel
            {
                Transaction = transaction,
                Categories = categories,
                DefCurrency = defCurrency
            };

            return View("TransactionForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                string UserGuid = User.Identity.GetUserId();
                var categories = _context.Users.Find(UserGuid).Categories.Where(c => c.IsSpendingCategory == transaction.IsSpending).OrderBy(c => c.Name);
                var defCurrency = _context.Users.Single(u => u.Id == UserGuid).DefCurrency;

                var viewModel = new TransactionFormViewModel()
                {
                    Transaction = transaction,                    
                    Categories = categories,
                    DefCurrency = defCurrency
                };

                return View("TransactionForm", viewModel);
            }

            if (transaction.Id == 0)
            {                
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

            //var categories = _context.Users.Find(UserGuid).Categories.Where(c=>c.IsSpendingCategory==transaction.IsSpending);

            int DefCatId = transaction.IsSpending ? Category.SpendingCategoryDefault : Category.IncomeCategoryDefault;

            var categories = _context.Users.Find(UserGuid).Categories.Where(c => c.IsSpendingCategory == transaction.IsSpending).ToList();
            var defCategory = categories.First(c => c.Id == DefCatId);
            categories.Remove(defCategory);
            categories = categories.OrderBy(c => c.Name).ToList();
            categories.Insert(0, defCategory);



            var viewModel = new TransactionFormViewModel
            {
                Transaction = transaction,
                Categories = categories,
                //IsSpending = transaction.IsSpending,
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
                CategoryId = Category.Rest
            };

            _context.Transactions.Add(transaction);
        }

        public FileResult ExportToExcel(DateTime? ExcelSince, DateTime? ExcelTill)
        {

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Транзакции");

                //создадим заголовки у столбцов
                worksheet.Cell("A" + 1).Value = "Дата";
                worksheet.Cell("B" + 1).Value = "Наименование";
                worksheet.Cell("C" + 1).Value = "Категория";
                worksheet.Cell("D" + 1).Value = "Сумма";

                worksheet.Range("A1:D1").Style.Fill.BackgroundColor = XLColor.FromArgb(59,89,113);
                worksheet.Range("A1:D1").Style.Font.FontColor = XLColor.White;
                
                // Заполняем данными
                string UserGuid = User.Identity.GetUserId();
                var transactions = from c in _context.Transactions.Include("Category")
                                   where c.UserId == UserGuid                                   
                                   select c;

                if (ExcelSince != null)
                    transactions = transactions.Where(t => t.TransDate >= ExcelSince);

                if (ExcelTill != null)
                    transactions = transactions.Where(t => t.TransDate <= ExcelTill);
                
                int k = 1;
                foreach(var t in transactions)
                {
                    k++;
                    worksheet.Cell("A" + k).Value = t.TransDate.ToShortDateString();
                    worksheet.Cell("B" + k).Value = t.Name;
                    if (t.Category != null)
                        worksheet.Cell("C" + k).Value = t.Category.Name;                    
                    if (t.IsSpending) {
                        worksheet.Cell("D" + k).Value = -t.Amount;
                        worksheet.Cell("D" + k).Style.Font.FontColor = XLColor.FromArgb(245, 105, 93);
                    }
                    else {
                        worksheet.Cell("D" + k).Value = t.Amount;
                        worksheet.Cell("D" + k).Style.Font.FontColor = XLColor.FromArgb(67, 172, 106);                        
                    }
                    worksheet.Cell("D" + k).Style.Font.Bold = true;
                }

                // пример создания сетки в диапазоне
                var rngTable = worksheet.Range("A2:D" + k);
                rngTable.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                rngTable.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                worksheet.Columns().AdjustToContents(); //ширина столбца по содержимому

                // вернем пользователю файл без сохранения его на сервере
                using (MemoryStream stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MyBudget.xlsx");
                }


            };
            
            
        }



    }
}