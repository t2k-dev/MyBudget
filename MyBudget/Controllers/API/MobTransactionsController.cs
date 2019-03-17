using AutoMapper;
using MyBudget.Dtos;
using MyBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyBudget.Controllers.API
{
    [Route("api/MobTransactions")]
    public class MobTransactionsController : ApiController
    {
        private ApplicationDbContext _context;

        public MobTransactionsController()
        {
            _context = new ApplicationDbContext();
        }

        
        [HttpGet]
        public IHttpActionResult GetTransactions(string id,int year, int month)
        {
            try
            {
                var transactions = _context.Transactions.Where(t => t.UserId == id && t.TransDate.Year == year && t.TransDate.Month == month).ToList();

                var result = transactions;

                if (result.Count() < 1)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                
            }
            return BadRequest();
        }

        public class TransactionDTO
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Amount { get; set; }
            public DateTime TransDate { get; set; }            
            public int? CategoryId { get; set; }
            public bool IsSpending { get; set; }
            public string Description { get; set; }
            public bool IsPlaned { get; set; }
            public string UserId { get; set; }
        }

        [HttpPost]
        public IHttpActionResult Add([FromBody]TransactionDTO request)
        {
            string errorMessage = null;
            try
            {
                Transaction transaction = new Transaction()
                {                    
                    Name = request.Name,
                    Amount = request.Amount,
                    TransDate = request.TransDate,
                    CategoryId = request.CategoryId,
                    IsSpending = request.IsSpending,
                    Description = request.Description,
                    IsPlaned = request.IsPlaned,
                    UserId = request.UserId
                };

                _context.Transactions.Add(transaction);
                _context.SaveChanges();

                return Ok(transaction.Id);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return BadRequest(errorMessage);
        }


        [HttpDelete]
        public IHttpActionResult DeleteTransaction(int id)
        {
            try
            {
                var transaction = _context.Transactions.SingleOrDefault(t => t.Id == id);
                if (transaction == null)
                    return NotFound();

                _context.Transactions.Remove(transaction);
                _context.SaveChanges();                                    

                return Ok($"Transaction id = {id} is successfully deleted");
            }
            catch (Exception)
            {

            }
            return BadRequest();
        }


    }
}
