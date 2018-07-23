using MyBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyBudget.Controllers.API
{
    public class TransactionsController : ApiController
    {
        private ApplicationDbContext _context;

        public TransactionsController()
        {
            _context = new ApplicationDbContext();
        }

        //PUT Меняет статус транзакции, плановая или осуществленная
        [HttpPut]
        public void SwitchPlaned(int Id)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var transactionInDb = _context.Transactions.SingleOrDefault(t => t.Id == Id);

            if (transactionInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            transactionInDb.IsPlaned = !transactionInDb.IsPlaned;
            _context.SaveChanges();
        }


        [HttpDelete]
        public void Delete(int Id)
        {
            var transactionInDb = _context.Transactions.SingleOrDefault(t => t.Id == Id);

            if (transactionInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Transactions.Remove(transactionInDb);
            _context.SaveChanges();

        }
    }
}
