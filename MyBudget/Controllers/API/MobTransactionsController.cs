using AutoMapper;
using MyBudget.Dtos;
using MyBudget.FiltersApi;
using MyBudget.Models;
using MyBudget.Models.ApiDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
        public IHttpActionResult GetTransactions(string id, int year, int month)
        {
            try
            {
                var transactions = _context.Transactions.Where(t => t.UserId == id && t.TransDate.Year == year && t.TransDate.Month == month).OrderBy(t=> t.TransDate).ToList();

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


        /// <summary>
        /// Add new transaction
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        [ValidateModel]
        public async Task<IHttpActionResult> Add([FromBody]TransactionCreateRequestDTO model)
        {
            try
            {
                if (model == null)
                    return BadRequest("You've sent an empty model");

                Transaction transaction = new Transaction()
                {
                    Name = model.Name,
                    Amount = model.Amount,
                    TransDate = model.TransDate,
                    CategoryId = model.CategoryId,
                    IsSpending = model.IsSpending,                    
                    IsPlaned = model.IsPlaned,
                    UserId = model.UserId
                };

                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();

                return Created("", transaction.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Edit transaction
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>        
        [HttpPut]
        [ValidateModel]
        [Route("api/MobTransactions/{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]TransactionUpdateRequestDTO model)
        {            
            try
            {
                if (model == null)
                    return BadRequest("You've sent an empty model");

                var TransactionInDB = _context.Transactions.SingleOrDefault(t => t.Id == id);
                if (TransactionInDB == null)
                    return NotFound();

                TransactionInDB.Name = model.Name ?? TransactionInDB.Name;
                TransactionInDB.Amount = model.Amount ?? TransactionInDB.Amount;
                TransactionInDB.CategoryId = model.CategoryId;
                TransactionInDB.IsPlaned = model.IsPlaned ?? TransactionInDB.IsPlaned;
                TransactionInDB.TransDate = model.TransDate ?? TransactionInDB.TransDate;

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
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
