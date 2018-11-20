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

        public ActionResult AddGoal()
        {
            string UserGuid = User.Identity.GetUserId();

            var goal = new Goal
            {
                Type = 1, /*Цель*/
                UserId = UserGuid
            };

            var viewModel = new GoalFormViewModel
            {
                Goal = goal,
                DefCurrency = _context.Users.SingleOrDefault(u => u.Id == UserGuid).DefCurrency
            };

            return View("GoalForm",viewModel);
        }

        public ActionResult AddDebt()
        {
            string UserGuid = User.Identity.GetUserId();

            var goal = new Goal
            {
                Type = 2, /*Взять в долг*/
                UserId = UserGuid
            };

            var viewModel = new GoalFormViewModel
            {
                Goal = goal,
                DefCurrency = _context.Users.SingleOrDefault(u => u.Id == UserGuid).DefCurrency
            };

            return View("GoalForm", viewModel);
        }

        public ActionResult AddCredit()
        {
            string UserGuid = User.Identity.GetUserId();

            
            var goal = new Goal
            {
                Type = 3, /*Дать в долг*/
                UserId = UserGuid
            };

            var viewModel = new GoalFormViewModel
            {
                Goal = goal,
                DefCurrency = _context.Users.SingleOrDefault(u => u.Id == UserGuid).DefCurrency
            };

            return View("GoalForm", viewModel);
        }


        public ActionResult Edit(int id)
        {
            string UserGuid = User.Identity.GetUserId();            
            var goal = _context.Goals.SingleOrDefault(g => g.Id == id);

            var viewModel = new GoalFormViewModel
            {
                Goal = goal,
                DefCurrency = _context.Users.SingleOrDefault(u => u.Id == UserGuid).DefCurrency
            };

            return View("GoalForm", viewModel);
        }

        [HttpPost]
        public ActionResult SaveGoal(Goal goal)
        {
            if (!ModelState.IsValid)
            {
                string UserGuid = User.Identity.GetUserId();

                var viewModel = new GoalFormViewModel
                {
                    Goal = goal,
                    DefCurrency = _context.Users.SingleOrDefault(u => u.Id == UserGuid).DefCurrency
                };

                return View("GoalForm", viewModel);
            }


            if (goal.Id == 0)
            {
                _context.Goals.Add(goal);

                if (goal.Type==Goal.TypeCredit) //Дать в долг
                {
                    Transaction transaction = new Transaction
                    {
                        Amount = goal.Amount,
                        CategoryId = _context.Categories.SingleOrDefault(c=> c.CreatedBy == "SYS_4").Id,
                        IsSpending = true,
                        Name = "Дать в долг \"" + goal.GoalName + "\"",
                        UserId = User.Identity.GetUserId(),
                        TransDate = DateTime.Now,
                        IsPlaned = false
                    };
                    _context.Transactions.Add(transaction);

                }
                else if (goal.Type == Goal.TypeDebt) //Взять в долг
                {
                    Transaction transaction = new Transaction
                    {
                        Amount = goal.Amount,
                        CategoryId = _context.Categories.SingleOrDefault(c => c.CreatedBy == "SYS_6").Id,
                        IsSpending = false,
                        Name = "Взять в долг \"" + goal.GoalName + "\"",
                        UserId = User.Identity.GetUserId(),
                        TransDate = DateTime.Now,
                        IsPlaned = false
                    };
                    _context.Transactions.Add(transaction);

                }




            }
            else /*Редактирование*/
            {
                var goalInDb = _context.Goals.Single(g => g.Id == goal.Id);
                goalInDb.GoalName = goal.GoalName;
                goalInDb.Amount = goal.Amount;
                goalInDb.CurAmount = goal.CurAmount;                
                goalInDb.CompleteDate = goal.CompleteDate;
                goalInDb.IsActive = goal.IsActive;                
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