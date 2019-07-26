using MyBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBudget.ViewModels
{
    public class TemplateFormViewModel
    {
        public Template Template { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public SelectList Days { get; set; }

        public string DefCurrency { get; set; }        
    }
}