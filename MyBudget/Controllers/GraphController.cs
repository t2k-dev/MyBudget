using Microsoft.AspNet.Identity;
using MyBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBudget.Controllers
{
    public class GraphController : Controller
    {
        
        public ActionResult Pie()
        {
            string UserGuid = User.Identity.GetUserId();
            GraphPie graphPie = new GraphPie(UserGuid);
            return View(graphPie);
        }
    }
}