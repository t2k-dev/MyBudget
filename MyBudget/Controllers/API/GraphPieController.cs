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
    public class GraphPieController : ApiController
    {
        
        //GET api/graphpie/?term=# 1 - за всё время, 2 - за месяц;

        public IEnumerable<GraphItem> GetGraphPie(int Term)
        {
            string UserGuid = User.Identity.GetUserId();
            GraphPie graphPie = new GraphPie(UserGuid,Term);
            return graphPie.GraphDataList;
        }
        


    }


}
