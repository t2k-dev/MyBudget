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


        [HttpPost]
        public ActionResult SaveDebt(Goal goal)
        {
            if (goal.Id == 0)
            {
                goal.Type = 3; //Для новых определяем что это долг
                goal.UserId = User.Identity.GetUserId();
                _context.Goals.Add(goal);
            }
            else
            {
                /*var transactionInDb = _context.Transactions.Single(t => t.Id == transaction.Id);
                transactionInDb.Name = transaction.Name;
                transactionInDb.Amount = transaction.Amount;*/
            }
            _context.SaveChanges();
            return RedirectToAction("MyBudget", "Transactions");
        }

        [HttpPost]
        public ActionResult SaveGoal(Goal goal)
        {
            if (goal.Id == 0)
            {
                goal.Type = 1; //Для новых определяем что это не долг
                goal.UserId = User.Identity.GetUserId();
                _context.Goals.Add(goal);
            }
            else
            {
                /*var transactionInDb = _context.Transactions.Single(t => t.Id == transaction.Id);
                transactionInDb.Name = transaction.Name;
                transactionInDb.Amount = transaction.Amount;*/
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
    }
}