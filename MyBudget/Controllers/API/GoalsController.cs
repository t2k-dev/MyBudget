using MyBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public IHttpActionResult AddGoal([FromBody] Goal goal)
        {
            try
            {
                _context.Goals.Add(goal);
                _context.SaveChanges();
                
                //TODO: надо сделать 201 code
                return Ok(goal.Id);
            }
            catch (Exception ex)
            {
                return BadRequest($"Exception:{ex.Message}");
            }            
        }

        [HttpDelete]
        public IHttpActionResult DeleteGoal(int id)
        {
            string errorMessage = null;
            try
            {
                var goal = _context.Goals.SingleOrDefault(t => t.Id == id);
                if (goal == null)
                    return NotFound();

                _context.Goals.Remove(goal);
                _context.SaveChanges();

                return Ok($"Goal id = {id} is successfully deleted");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return BadRequest(errorMessage);
        }


    }
}
