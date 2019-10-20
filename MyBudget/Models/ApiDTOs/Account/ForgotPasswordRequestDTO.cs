using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudget.Models.ApiDTOs.Account
{
    public class ForgotPasswordRequestDTO
    {
        public string Email { get; set; }
        public string Username { get; set; }
    }
}