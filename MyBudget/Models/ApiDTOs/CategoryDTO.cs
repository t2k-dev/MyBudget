using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudget.Models.ApiDTOs
{
    /// <summary>
    /// User categories DTO
    /// </summary>
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSpendingCategory { get; set; }
        public bool IsSystem { get; set; }
        public string CreatedBy { get; set; }
        public string Icon { get; set; }
    }
}
