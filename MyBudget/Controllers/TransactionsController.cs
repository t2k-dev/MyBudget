using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBudget.Controllers
{
    public class TransactionsController : Controller
    {
        // GET: Transactions
        public ActionResult MyList()
        {
            return View();
        }
    }
}