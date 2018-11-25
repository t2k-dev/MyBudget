using AutoMapper;
using Microsoft.AspNet.Identity;
using MyBudget.Dtos;
using MyBudget.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        //GET api/transactions

        public IEnumerable<TransactionDto> GetTransactions(int year,int month) 
        {            
            string UserGuid = User.Identity.GetUserId();
            //var transactions = _context.Transactions.Where(m => (m.UserId == UserGuid)).ToList().Where(m => m.TransDate.ToString("MMyyyy") == MMyyyy).ToList().OrderByDescending(m=> m.TransDate);
            var transactions = from c in _context.Transactions.Include("Category")
                               where c.UserId == UserGuid && c.TransDate.Year == year && c.TransDate.Month == month
                               orderby c.TransDate descending
                               select c;
            return transactions.Select(Mapper.Map<Transaction,TransactionDto>);
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
