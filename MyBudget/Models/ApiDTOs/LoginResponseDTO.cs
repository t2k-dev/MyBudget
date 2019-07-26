using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudget.Models.ApiDTOs
{
    /// <summary>
    /// Login response model for Web Api
    /// </summary>
    public class LoginResponseDTO
    {
        /*Временно*/
        public int Status { get; set; }

        public string UserId { get; set; }

        /*Configs*/
        public string DefCurrency { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool CarryOverRests { get; set; }
        public bool UseTemplates { get; set; }

    }
}