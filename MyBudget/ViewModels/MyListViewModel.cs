using MyBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudget.ViewModels
{    

    public class MyListViewModel
    {

        public List<Transaction> MyTransactions { get; set; }
        public double Rest {
            get {
                return MyTransactions.Where(x => x.IsSpending == false).Sum(x => x.Amount) - MyTransactions.Where(x => x.IsSpending == true).Sum(x => x.Amount);
            }
        }
    }
}