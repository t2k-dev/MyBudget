using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBudget.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }
        public DateTime TransDate { get; set; }

        public Category Category { get; set; }
        public byte CategoryId { get; set; }

        public bool IsSpending { get; set; }       
        public string Description { get; set; }

    }
}