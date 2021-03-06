﻿using MyBudget.BusinessLogic;
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
                var GoalsList = _context.Goals.Where(m => m.UserId == Id).ToList();

                if (GoalsList.Count() < 1)
                    return NotFound();

                return Ok(GoalsList);
            }
            catch (Exception)
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
                GoalInDb.CurAmount = model.CurAmount ?? GoalInDb.CurAmount;
                GoalInDb.CompleteDate = model.CompleteDate ?? GoalInDb.CompleteDate;

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
