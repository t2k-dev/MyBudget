using MyBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudget.BusinessLogic
{
    /// <summary>
    /// Business Logic for Goales
    /// </summary>
    public class GoalService
    {
        #region ctor & variables

        private ApplicationDbContext _context;
        private Goal _goal;

        public GoalService(int goalId)
        {
            _context = new ApplicationDbContext();
            _goal = _context.Goals.SingleOrDefault(g => g.Id == goalId);
            if (_goal == null)
                throw new NullReferenceException();
        }

        #endregion

        public void PutMoney(double amount)
        {            

            _goal.CurAmount += amount;

            if (_goal.Amount == _goal.CurAmount)
                _goal.IsActive = false;

            int categoryId = 0;
            if (_goal.Type == Goal.TypeDebt)
                categoryId = Category.PayCredit;
            else if (_goal.Type == Goal.TypeCredit)
                categoryId = Category.RecieveDebt;
            else if (_goal.Type == Goal.TypeGoal)
                categoryId = Category.PayGoal;


            Transaction transaction = new Transaction
            {
                Amount = amount,
                CategoryId = categoryId,
                IsSpending = _goal.Type == Goal.TypeCredit ? false : true,
                Name = "Пополнение для \"" + _goal.GoalName + "\"",
                UserId = _goal.UserId,
                TransDate = DateTime.Now,
                IsPlaned = false
            };
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

    }
}
