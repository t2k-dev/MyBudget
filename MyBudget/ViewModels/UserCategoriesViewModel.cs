using MyBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudget.ViewModels
{
    public class UserCategoriesViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}