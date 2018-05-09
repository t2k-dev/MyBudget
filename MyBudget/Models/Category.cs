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

        [Required]
        [MaxLength(50)]        
        public String Name { get; set; }
        public bool IsSpendingCategory { get; set; }
        public string Icon { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public Category()
        {
            Users = new List<ApplicationUser>();
        }

        [MaxLength(128)]
        public string CreatedBy { get; set; }
    }
}