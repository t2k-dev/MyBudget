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

        public ActionResult GoalForm()
        {
            string UserGuid = User.Identity.GetUserId();
            ViewBag.DefCurrency = _context.Users.SingleOrDefault(u => u.Id == UserGuid).DefCurrency;

            Goal goal = new Goal
            {
                UserId = UserGuid,
                Type = 1
            };

            return View(goal);
        }

        public ActionResult DebtForm()
        {
            string UserGuid = User.Identity.GetUserId();
            ViewBag.DefCurrency = _context.Users.SingleOrDefault(u => u.Id == UserGuid).DefCurrency;

            Goal goal = new Goal
            {
                UserId = UserGuid,
                Type = 2
            };

            return View(goal);
        }

        public ActionResult CreditForm()
        {
            string UserGuid = User.Identity.GetUserId();
            ViewBag.DefCurrency = _context.Users.SingleOrDefault(u => u.Id == UserGuid).DefCurrency;

            Goal goal = new Goal
            {
                UserId = UserGuid,
                Type = 3
            };

            return View(goal);

        }

        [HttpPost]
        public ActionResult SaveGoal(Goal goal)
        {
            if (!ModelState.IsValid)
            {
                string formName = "";
                switch (goal.Type) {
                    case 1:
                        formName = "GoalForm";
                        break;
                    case 2:
                        formName = "DebtForm";
                        break;
                    case 3:
                        formName = "CreditForm";
                        break;
                }
                return View(formName, goal);
            }


            if (goal.Id == 0)
            {
                _context.Goals.Add(goal);

                /*if (goal.Type==Goal.TypeDebt)
                {
                    Transaction transaction = new Transaction
                    {
                        Amount = goal.Amount,
                        CategoryId = ,
                        IsSpending = cat.IsSpendingCategory,
                        Name = "Пополнение для \"" + goal.GoalName + "\"",
                        UserId = User.Identity.GetUserId(),
                        TransDate = DateTime.Now,
                        IsPlaned = false
                    };
                    _context.Transactions.Add(transaction);

                }*/


            }
            else
            {
                /*Редактирование*/
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

            Transaction transaction = new Transaction
            {
                Amount = Amount,
                CategoryId = cat.Id,
                IsSpending = cat.IsSpendingCategory,
                Name = "Пополнение для \"" + goal.GoalName + "\"",
                UserId = User.Identity.GetUserId(),
                TransDate = DateTime.Now,
                IsPlaned = false
            };
            _context.Transactions.Add(transaction);

            _context.SaveChanges();
            return RedirectToAction("MyBudget", "Transactions");
        }
    }
}