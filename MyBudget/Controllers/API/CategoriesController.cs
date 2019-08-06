using MyBudget.BusinessLogic;
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
    public class CategoriesController : ApiController
    {
        private ApplicationDbContext _context;

        public CategoriesController()
        {
            _context = new ApplicationDbContext();
        }
                
        [HttpGet]
        [Route("api/Categories")]
        public IHttpActionResult GetCategories(string userId)
        {
            try
            {
                var categories = _context.Users.Find(userId).Categories.ToList();

                if (categories == null)
                    return NotFound();

                List<CategoryDTO> ReturnCatList = new List<CategoryDTO>();
                foreach (var item in categories)
                {
                    ReturnCatList.Add(new CategoryDTO
                    {
                        Id = item.Id,
                        Name = item.Name,
                        IsSpendingCategory = item.IsSpendingCategory,
                        IsSystem = item.IsSystem,
                        CreatedBy = item.CreatedBy,
                        Icon = item.Icon
                    });

                }
                return Ok(ReturnCatList);
            }
            catch (Exception)
            {

            }
            return BadRequest();
        }

        [HttpPost]
        [Route("api/Categories")]
        public IHttpActionResult Post([FromBody]CategoryDTO model)
        {
            string errorMessage = null;
            try
            {                
                Category category = new Category()
                {
                    Name = model.Name,
                    IsSpendingCategory = model.IsSpendingCategory,                    
                    CreatedBy = model.CreatedBy,
                    Icon = model.Icon
                };
                category.Users.Add(_context.Users.Find(model.CreatedBy));

                _context.Categories.Add(category);
                _context.SaveChanges();
                //TODO: location URL
                return Created("", category.Id);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return BadRequest(errorMessage);
        }


        [HttpPut]
        [Route("api/Categories/{id}")]
        public IHttpActionResult Put(int id, [FromBody]CategoryDTO model)
        {
            string errorMessage = null;

            try
            {
                var oldCategory = _context.Categories.Single(c => c.Id == id);

                oldCategory.Name = model.Name;
                oldCategory.Icon = model.Icon;

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("api/Categories/{id}")]
        public IHttpActionResult Delete(int id, string userId)
        {
            try
            {
                var categoryService = new CategoryService(id, userId);
                categoryService.DeleteCategory();

                return Ok($"Category id = {id} is successfully deleted for UserId = {userId}");
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
