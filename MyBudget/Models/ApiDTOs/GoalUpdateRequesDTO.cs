using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBudget.Models.ApiDTOs
{
    /// <summary>
    /// Goal update request model for Web Api
    /// </summary>
    public class GoalUpdateRequesDTO
    {
        [MinLength(1)]
        public string GoalName { get; set; }
        public double? Amount { get; set; }
        public double? CurAmount { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CompleteDate { get; set; }
    }
}