using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudget.Models.ApiDTOs
{
    public class ChangePasswordDTO
    {
        public string UserId { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

    }
}