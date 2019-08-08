using MyBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudget.BusinessLogic
{
    public class GraphService
    {
        #region ctor & variables

        private ApplicationDbContext _context;
        private string _userId;        
        private string[] _arrColors = {
                        "#ff3d67",
                        "#059bff",
                        "#ffc233",
                        "#CE93D8",
                        "#ff9124",
                        "#22cece",
                        "#7CB342",
                        "#FF5722",
                        "#D4E157",
                        "#26A69A",
                        "#9575CD",
                        "#81D4FA"
            };



        public GraphService(string userId)
        {
            _context = new ApplicationDbContext();            
            _userId = userId;
        }

        #endregion

        public List<GraphItem> GetSpendingGraphByCategory(DateTime since, DateTime till)
        {
            var categories = _context.Users.Find(_userId).Categories.Where(c => c.IsSpendingCategory == true);
            var transactions = _context.Transactions.Where(t => t.UserId == _userId && t.TransDate >= since && t.TransDate <= till);
            List<GraphItem> resultGraphList = new List<GraphItem>();

            int i = 0;
            foreach (var cat in categories)
            {
                GraphItem item = new GraphItem();
                item.Amount = transactions.Where(t => t.CategoryId == cat.Id).ToList().Sum(s => s.Amount);
                if (item.Amount > 0)
                {
                    item.Caption = cat.Name;
                    item.Color = _arrColors[i];
                    resultGraphList.Add(item);
                    i++;
                }
            }
            return resultGraphList;
        }


        /*
        //Строка значений
        private string AmountsString()
        {            
            var amountArray = _graph.Select(t => t.Amount).ToArray();                        
            return String.Join(",", amountArray); 
        }

        //Строка заголовков
        public string CaptionsString()
        {
            string sumStr = "";
            foreach (var item in GraphDataList)
            {
                sumStr += "'" + item.Caption + "',";
            }
            sumStr = sumStr.TrimEnd(',');
            return sumStr;
        }

        //Строка цветов
        public string ColorsString()
        {
            string sumStr = "";
            foreach (var item in GraphDataList)
            {
                sumStr += "'" + item.Color + "',";
            }
            sumStr = sumStr.TrimEnd(',');
            return sumStr;
        }*/
    }
}
