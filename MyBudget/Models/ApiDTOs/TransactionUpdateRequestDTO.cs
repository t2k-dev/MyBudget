using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBudget.Models.ApiDTOs
{
    public class TransactionUpdateRequestDTO
    {
        public string Name { get; set; }
        public double? Amount { get; set; }
        public DateTime? TransDate { get; set; }
        [Required]
        //TODO Убрать обязательность, как добавим категории "Без категории"
        public int? CategoryId { get; set; }        
        public bool? IsPlaned { get; set; }        
    }
}