using MyBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudget.ViewModels
{
    public class TransactionFormViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public Transaction Transaction { get; set; }
    }
}