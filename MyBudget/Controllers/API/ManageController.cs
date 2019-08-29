using Microsoft.AspNet.Identity;
using MyBudget.FiltersApi;
using MyBudget.Models;
using MyBudget.Models.ApiDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyBudget.Controllers.API
{
    public class ManageController : BaseApiController
    {
        private ApplicationDbContext _context;

        public ManageController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPut]
        [Route("api/manage/setdefaultcurrency/{curChar}")]
        public void SetDefaultCurrency(string curChar)
        {
            string UserGuid = User.Identity.GetUserId();
            _context.Users.SingleOrDefault(u => u.Id == UserGuid).DefCurrency = curChar;
            _context.SaveChanges();

        }

        [HttpPost]
        [ValidateModel]
        [Route("api/manage/changepassword")]
        public async Task<IHttpActionResult> ChangePassword([FromBody]ChangePasswordDTO model)
        {
            try
            {
                if (model == null)
                    return BadRequest("You've sent an empty model");

                var result = await this.AppUserManager.ChangePasswordAsync(model.UserId, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [Route("api/manage/sendPasswordEmail")]
        public IHttpActionResult sendPasswordEmail(string id)
        {
           
            var senderEmail = new MailAddress("MyBudgetTeam@yandex.kz", "MyBudgetTeam");
            var password = "nekruz";

            var receiverEmail = new MailAddress("t2k.ivan@gmail.com", "Receiver");
            
            var smtp = new SmtpClient
            {
                Host = "smtp.yandex.ru",
                Port = 25,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };

            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            var pass = user.PasswordHash;

            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = "MyBudget восстановление пароля",
                Body = "Ваш пароль: "
            })
            {
                smtp.Send(mess);
            }

            return Ok();
        }

    }
}
