using MyBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudget.BusinessLogic
{
    public class MonthlyOpsService
    {
        #region ctor,  variables, functions

        private ApplicationDbContext _context;
        private ApplicationUser _user;
        private string _userId;
        private DateTime? _updateDate;

        public MonthlyOpsService(string userId)
        {
            _context = new ApplicationDbContext();
            _user = _context.Users.SingleOrDefault(u => u.Id == userId);
        }

        #endregion

        #region methods

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

        private void AddRestTransaction()
        {
            DateTime PrevMonth = DateTime.Now.AddMonths(-1);
            var transactions = _context.Transactions
                    .Where(t => (t.UserId == _user.Id) &&
                                (t.IsPlaned == false) &&
                                (t.TransDate.Month == PrevMonth.Month) && (t.TransDate.Year == PrevMonth.Year)
                          ).ToList();

            double sum = transactions.Where(x => x.IsSpending == false).Sum(x => x.Amount) - transactions.Where(x => x.IsSpending == true).Sum(x => x.Amount);

            if (sum <= 0)
                return;

            var transaction = new Transaction
            {
                Amount = sum,
                IsPlaned = false,
                IsSpending = false,
                Name = "Остаток за прошлый месяц",
                UserId = _user.Id,
                TransDate = DateTime.Now,
                CategoryId = Category.Rest
            };

            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        #endregion
        public void ExecuteMonthlyOps()
        {
            if (UpdateDateExpired(_user.UpdateDate))
            {
                if (_user.CarryoverRests)
                    AddRestTransaction();
                _user.UpdateDate = DateTime.Now;
                _context.SaveChanges();
            }
        }
    }
}