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

        [Required (ErrorMessage="Введите название")]
        [Display(Name = "Название")]
        public string GoalName { get; set; }

        [Required]
        public byte Type { get; set; }

        [Required(ErrorMessage = "Введите сумму")]
        [Range(1, 100000000, ErrorMessage ="Введите Сумму")]
        [Display(Name = "Сумма")]
        public double Amount { get; set; } /*Общая сумма*/

        public double CurAmount { get; set; } /*Накопленная сумма*/

        public bool IsActive { get; set; }

        public string Description { get; set; }

        
        public ApplicationUser User { get; set; }
        [Required]
        public string UserId { get; set; }

        public DateTime? CompleteDate { get; set; }

        public static readonly byte TypeGoal = 1;
        public static readonly byte TypeDebt = 2;
        public static readonly byte TypeCredit = 3;
    }
}