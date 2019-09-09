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
    public class TemplateController : ApiController
    {
        private ApplicationDbContext _context;

        public TemplateController()
        {
            _context = new ApplicationDbContext();
        }


        [HttpGet]
        public async Task<IHttpActionResult> GetTemplates(string id)
        {
            try
            {
                var templates = _context.Templates.Where(t => t.UserId == id).OrderBy(t => t.Day).ToList();

                var result = templates;

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
        /// Add new template
        /// </summary>        
        [HttpPost]
        [ValidateModel]
        public async Task<IHttpActionResult> Add([FromBody]TemplateCreateRequestDTO model)
        {
            try
            {
                if (model == null)
                    return BadRequest("You've sent an empty model");

                Template template = new Template()
                {
                    Name = model.Name,
                    Amount = model.Amount,
                    Day = model.Day,
                    CategoryId = model.CategoryId,
                    IsSpending = model.IsSpending,                    
                    UserId = model.UserId
                };

                _context.Templates.Add(template);
                await _context.SaveChangesAsync();

                return Created("", template.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Edit template
        /// </summary>
        /// <param name="id"></param>            
        [HttpPut]
        [ValidateModel]
        [Route("api/Template/{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]TemplateCreateRequestDTO model)
        {
            try
            {
                if (model == null)
                    return BadRequest("You've sent an empty model");

                var TemplateInDB = _context.Templates.SingleOrDefault(t => t.Id == id);
                if (TemplateInDB == null)
                    return NotFound();

                TemplateInDB.Name = model.Name;
                TemplateInDB.Amount = model.Amount;
                TemplateInDB.CategoryId = model.CategoryId;
                TemplateInDB.Day = model.Day;

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteTemplate(int id)
        {
            try
            {
                var template = _context.Templates.SingleOrDefault(t => t.Id == id);
                if (template == null)
                    return NotFound();

                _context.Templates.Remove(template);
                _context.SaveChanges();

                return Ok($"Template id = {id} is successfully deleted");
            }
            catch (Exception)
            {

            }
            return BadRequest();
        }

    }
}
