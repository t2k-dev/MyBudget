using MyBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyBudget.Controllers.API
{
    public class MobGraphsController : ApiController
    {
        private ApplicationDbContext _context;

        public MobGraphsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        [Route("api/pieGraph")]
        public IHttpActionResult GetPieGraph(string id, int term)
        {            
            try
            {                
                GraphPie graphPie = new GraphPie(id, term);

                var result =  graphPie.GraphDataList;

                if (result.Count() < 1)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {

            }
            return BadRequest();
        }
    }
}
