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
        public double Rest { get; set; }
    }
}