using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBudget.Models
{
    public class Category
    {        
        public int Id { get; set; }

        [Required(ErrorMessage ="Введите название")]
        [MaxLength(50)]        
        public String Name { get; set; }

        public bool IsSpendingCategory { get; set; }

        public string Icon { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        [MaxLength(128)]
        public string CreatedBy { get; set; }

        public bool IsSystem { get; set; }

        public Category()
        {
            Users = new List<ApplicationUser>();
        }

        #region System Categories

        /// <summary>
        /// "Без Категории" for income transactions Id = 1
        /// </summary>
        public static readonly int IncomeCategoryDefault = 1;

        /// <summary>
        /// "Без Категории" for spending transactions Id = 2
        /// </summary>
        public static readonly int SpendingCategoryDefault = 2;
        
        /// <summary>
        /// Take money from somebody
        /// </summary>
        public static readonly int TakeDebt = 3;

        /// <summary>
        /// Give money to somebody
        /// </summary>
        public static readonly int GiveCredit = 4;

        /// <summary>
        /// Rest from the previous month
        /// </summary>
        public static readonly int Rest = 5;

        /// <summary>
        /// Give borrowed money
        /// </summary>
        public static readonly int PayCredit = 6;

        /// <summary>
        ///  Return given money
        /// </summary>
        public static readonly int RecieveDebt = 7;

        /// <summary>
        /// Put money for a Goal
        /// </summary>
        public static readonly int PayGoal = 8;


        #endregion
    }
}