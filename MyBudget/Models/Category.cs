﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBudget.Models
{
    public class Category
    {
        public byte Id { get; set; }

        [Required]
        [MaxLength(50)]        
        public String Name { get; set; }
    }
}