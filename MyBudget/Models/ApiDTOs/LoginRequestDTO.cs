using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBudget.Models.ApiDTOs
{
    /// <summary>
    /// Login request model for Web Api
    /// </summary>
    public class LoginRequestDTO
    {
        [Required]
        public string usr { get; set; }
        [Required]
        public string pass { get; set; }
    }
}