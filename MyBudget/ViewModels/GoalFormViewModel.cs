using MyBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudget.ViewModels
{
    public class GoalFormViewModel
    {
        public Goal Goal { get; set; }
        public string DefCurrency { get; set; }
    }
}