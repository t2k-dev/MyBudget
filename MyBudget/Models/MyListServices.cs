using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudget.Models
{
    public class MyListServices
    {
        private double rest;
        private double planedRest;

        public double Rest { get { return rest; } }
        public double PlanedRest { get { return planedRest; } }



        public MyListServices(List<Transaction> Transactions)
        {
            planedRest = Transactions.Where(x => x.IsSpending == false).Sum(x => x.Amount) - Transactions.Where(x => x.IsSpending == true).Sum(x => x.Amount); /*Запланированный*/
            Transactions = Transactions.Where(t => t.IsPlaned == false).ToList();
            rest = Transactions.Where(x => x.IsSpending == false).Sum(x => x.Amount) - Transactions.Where(x => x.IsSpending == true).Sum(x => x.Amount); /*Текущий*/
        }
    }
}