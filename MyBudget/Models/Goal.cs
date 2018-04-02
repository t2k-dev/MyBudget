using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBudget.Models
{
    public class Goal
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Amount { get; set; } /*Общая сумма*/
        public double CurAmount { get; set; } /*Накопленная сумма*/
        public bool IsDebt { get; set; }
        public string Description { get; set; }        
    }
}