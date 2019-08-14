using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace MyBudget.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Category> Categories { get; set; }
        [MaxLength(3)]
        public string DefCurrency { get; set; }
        public bool CarryoverRests { get; set; }
        public bool UseTemplates { get; set; }
        public DateTime? UpdateDate { get; set; }


        public ApplicationUser()
        {
            Categories = new List<Category>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }

    }
}