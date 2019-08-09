﻿using Microsoft.AspNet.Identity;
using MyBudget.BusinessLogic;
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
        
        [Route("api/graphpie/{term}")]
        public IEnumerable<GraphItem> GetGraphPie(int Term)
        {
            string UserGuid = User.Identity.GetUserId();
            GraphPie graphPie = new GraphPie(UserGuid,Term);
            return graphPie.GraphDataList;
        }

        /// <summary>
        /// Получить данные для графика затрат "Пирог" по категориям
        /// </summary>                
        [Route("api/graph/getSpendingGraph/{userId}/{since}/{till}" )]
        public IHttpActionResult GetSpendingGraph(string userId, string since, string till)
        {

            try
            {
                DateTime sinceParam = DateTime.ParseExact(since, "dd-MM-yyyy", null);
                DateTime tillParam = DateTime.ParseExact(till, "dd-MM-yyyy", null);

                var graphService = new GraphService(userId);
                List<GraphItem> resultList = graphService.GetSpendingGraphByCategory(sinceParam, tillParam); ;

                if (resultList.Count() < 1)
                    return NotFound();

                return Ok(resultList);
            }
            catch (Exception)
            {

            }
            return BadRequest();
        }


    }


}