﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBudget.Models
{
    public class Template
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Range(1, 100000000, ErrorMessage = "Сумма должна быть больше нуля")]
        [Required(ErrorMessage = "Укажите сумму")]
        public double Amount { get; set; }

        public int Day { get; set; }

        public Category Category { get; set; }
        public int? CategoryId { get; set; }

        [Required]
        public bool IsSpending { get; set; }       

        public ApplicationUser User { get; set; }
        [Required]
        [MaxLength(128)]
        public string UserId { get; set; }

    }
}