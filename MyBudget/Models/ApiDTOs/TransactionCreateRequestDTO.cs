using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBudget.Models.ApiDTOs
{
    /// <summary>
    /// Transaction create request model for Web Api
    /// </summary>
    public class TransactionCreateRequestDTO
    {        
        [Required]
        public double Amount { get; set; }

        [Required]
        public DateTime TransDate { get; set; }

        [Required]
        public bool IsSpending { get; set; }

        [Required]
        public string UserId { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public bool IsPlaned { get; set; }


    }
}