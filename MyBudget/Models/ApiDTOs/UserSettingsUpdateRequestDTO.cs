using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudget.Models.ApiDTOs
{
    public class UserSettingsUpdateRequestDTO
    {
        public string DefCurrency { get; set; }
        public bool? CarryoverRests { get; set; }
        public bool? UseTemplates { get; set; }
    }
}
