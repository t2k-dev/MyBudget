using MyBudget.BusinessLogic;
using MyBudget.FiltersApi;
using MyBudget.Models;
using MyBudget.Models.ApiDTOs;
using MyBudget.Models.ApiDTOs.Goals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyBudget.Controllers.API
{
    public class GoalsController : ApiController
    {
        private ApplicationDbContext _context;

        public GoalsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IHttpActionResult Goals(string Id)
        {
            try
            {
                var GoalsList = _context.Goals.Where(g => g.UserId == Id).ToList();

                if (GoalsList.Count() < 1)
                    return NotFound();

                List<GoalsListItemDTO> resultList = new List<GoalsListItemDTO>();
                foreach (var goal in GoalsList)
                {
                    GoalsListItemDTO itemDTO = new GoalsListItemDTO()
                    {
                        Id = goal.Id,
                        GoalName = goal.GoalName,
                        Type = goal.Type,
                        Amount = goal.Amount,
                        CurAmount = goal.CurAmount,
                        IsActive = goal.IsActive,
                        UserId = goal.UserId,
                        CompleteDate = goal.CompleteDate == null ? null : goal.CompleteDate.Value.ToString("yyyy.MM.dd")
                    };
                    resultList.Add(itemDTO);
                }
                return Ok(resultList);
            }
            catch (Exception e)
            {

            }
            return BadRequest();
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IHttpActionResult> AddGoal([FromBody] GoalCreateRequestDTO model)
        {
            try
            {
                if (model == null)
                    return BadRequest("You've sent an empty model");

                var goal = new Goal()
                {
                    GoalName = model.GoalName,
                    Amount = model.Amount,
                    Type = model.Type,
                    UserId = model.UserId,
                    IsActive = true,
                    CurAmount = model.CurAmount ?? 0,
                    CompleteDate = model.CompleteDate ?? null
                };

                _context.Goals.Add(goal);

                // Добавить транзакции
                if (goal.Type == Goal.TypeCredit) //Дать в долг
                {
                    Transaction transaction = new Transaction
                    {
                        Amount = goal.Amount,
                        CategoryId = Category.GiveCredit,
                        IsSpending = true,
                        Name = "Дать в долг \"" + goal.GoalName + "\"",
                        UserId = model.UserId,
                        TransDate = DateTime.Now,
                        IsPlaned = false
                    };
                    _context.Transactions.Add(transaction);
                }
                else if (goal.Type == Goal.TypeDebt) //Взять в долг
                {
                    Transaction transaction = new Transaction
                    {
                        Amount = goal.Amount,
                        CategoryId = Category.TakeDebt,
                        IsSpending = false,
                        Name = "Взять в долг \"" + goal.GoalName + "\"",
                        UserId = model.UserId,
                        TransDate = DateTime.Now,
                        IsPlaned = false
                    };
                    _context.Transactions.Add(transaction);
                }

                await _context.SaveChangesAsync();

                return Created("", goal.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ValidateModel]
        [Route("api/goals/{id}")]
        public async Task<IHttpActionResult> EditGoal(int id, [FromBody] GoalUpdateRequesDTO model)
        {
            try
            {
                var GoalInDb = _context.Goals.SingleOrDefault(g => g.Id == id);
                if (GoalInDb == null)
                    return NotFound();

                GoalInDb.GoalName = model.GoalName ?? GoalInDb.GoalName;
                GoalInDb.Amount = model.Amount ?? GoalInDb.Amount;
                GoalInDb.CurAmount = (double)model.CurAmount;
                GoalInDb.CompleteDate = model.CompleteDate;

                await _context.SaveChangesAsync();
                return Ok($"Goal id = {id} is successfully modified");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/goals/{id}")]
        public async Task<IHttpActionResult> DeleteGoal(int id)
        {
            try
            {
                var goal = _context.Goals.SingleOrDefault(t => t.Id == id);
                if (goal == null)
                    return NotFound();

                _context.Goals.Remove(goal);
                await _context.SaveChangesAsync();

                return Ok($"Goal id = {id} is successfully deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/goals/{id}/payGoal")]
        public IHttpActionResult PayGoal(int id, double amount)
        {
            try
            {
                GoalService goalService = new GoalService(id);
                goalService.PutMoney(amount);

                return Ok();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
