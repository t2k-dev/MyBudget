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

        [Required]
        public string Name { get; set; }

        [Required]
        public double Amount { get; set; }

        public DateTime TransDate { get; set; }

        public Category Category { get; set; }
        public byte CategoryId { get; set; }

        [Required]
        public bool IsSpending { get; set; }

        public string Description { get; set; }

        
        public ApplicationUser User { get; set; }
        [Required]
        public string UserId { get; set; }

    }
}