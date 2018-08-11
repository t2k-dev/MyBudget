using Microsoft.AspNet.Identity;
using MyBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyBudget.Controllers.API
{
    public class GraphController : ApiController
    {
        private ApplicationDbContext _context;


        public GraphController()
        {
            _context = new ApplicationDbContext();
        }


        public GraphPie GetPie()
        {
            string UserGuid = User.Identity.GetUserId();
            GraphPie graphPie = new GraphPie(UserGuid);
            return graphPie;
        }


    }


}
