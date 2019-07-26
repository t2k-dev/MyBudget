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
        /// "Без Категории" for income transactions Id = 30
        /// </summary>
        public static readonly int IncomeCategoryDefault = 30;

        /// <summary>
        /// "Без Категории" for spending transactions Id = 31
        /// </summary>
        public static readonly int SpendingCategoryDefault = 31;
        
        /// <summary>
        /// Rest from the previous month
        /// </summary>
        public static readonly int Rest = 5;

        /// <summary>
        /// Take money from somebody
        /// </summary>
        public static readonly int TakeDebt = 2025;

        /// <summary>
        /// Give money to somebody
        /// </summary>
        public static readonly int GiveCredit = 17;

        /// <summary>
        /// Put money for a Goal
        /// </summary>
        public static readonly int PayGoal = 19;

        /// <summary>
        /// Give borrowed money
        /// </summary>
        public static readonly int PayCredit = 16;

        /// <summary>
        ///  Return given money
        /// </summary>
        public static readonly int RecieveDebt = 2;



        #endregion
    }
}