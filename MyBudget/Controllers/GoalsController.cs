using Microsoft.AspNet.Identity;
using MyBudget.Models;
using MyBudget.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBudget.Controllers
{
    public class GoalsController : Controller
    {
        private ApplicationDbContext _context;

        public GoalsController()
        {
            _context = new ApplicationDbContext();
        }

        
        public ActionResult DebtForm()
        {                        
            return View();
        }

        public ActionResult GoalForm()
        {
            return View();
        }

        public ActionResult CreditForm()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SaveDebt(Goal goal)
        {
            if (goal.Id == 0)
            {
                goal.Type = 2; //Для новых определяем что это мой долг
                goal.UserId = User.Identity.GetUserId();
                _context.Goals.Add(goal);
            }
            else
            {
            }
            _context.SaveChanges();
            return RedirectToAction("MyBudget", "Transactions");
        }

        [HttpPost]
        public ActionResult SaveGoal(Goal goal)
        {
            if (goal.Id == 0)
            {
                goal.Type = 1; //Для новых определяем что это цель
                goal.UserId = User.Identity.GetUserId();
                _context.Goals.Add(goal);
            }
            else
            {
            }
            _context.SaveChanges();
            return RedirectToAction("MyBudget", "Transactions");
        }

        [HttpPost]
        public ActionResult SaveCredit(Goal goal)
        {
            if (goal.Id == 0)
            {
                goal.Type = 3; //Для новых определяем что это долг мне
                goal.UserId = User.Identity.GetUserId();
                _context.Goals.Add(goal);
            }
            else
            {
            }
            _context.SaveChanges();
            return RedirectToAction("MyBudget", "Transactions");
        }



        public ActionResult Delete(int id)
        {
            var goal = _context.Goals.SingleOrDefault(t => t.Id == id);
            if (goal == null)
                return HttpNotFound();

            _context.Goals.Remove(goal);
            _context.SaveChanges();

            return RedirectToAction("MyBudget", "Transactions");
        }

        [HttpPost]
        public ActionResult PutMoney(double Amount, int putOnId, string catType)
        {
            var goal = _context.Goals.Find(putOnId);
            var cat = _context.Categories.SingleOrDefault(c => c.CreatedBy == catType);

            goal.CurAmount += Amount;

            Transaction transaction = new Transaction();
            transaction.Amount = Amount;
            transaction.CategoryId = cat.Id;
            transaction.IsSpending = cat.IsSpendingCategory;
            transaction.Name = "Пополнение для \"" + goal.GoalName+"\"";
            transaction.UserId = User.Identity.GetUserId();
            transaction.TransDate = DateTime.Now;
            transaction.IsPlaned = true;
            _context.Transactions.Add(transaction);

            _context.SaveChanges();
            return RedirectToAction("MyBudget", "Transactions");
        }
    }
}