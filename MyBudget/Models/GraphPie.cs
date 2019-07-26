using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudget.Models
{

    public class GraphItem
    {
        public double Amount { get; set; }
        public string Color { get; set; }
        public string Caption { get; set; }
    }

    public class GraphPie
    {
        public List<GraphItem> GraphDataList { get; set; }
        //private List<GraphItem> graphDataList;
        
        public GraphPie(string UserGuid, int Term)
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            GraphDataList = new List<GraphItem>();

            string[] arrColors = {
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

            var categories = _context.Users.Find(UserGuid).Categories.Where(c => c.IsSpendingCategory == true);
            var transactions = _context.Transactions.Where(t => t.UserId == UserGuid);

            if (Term == 1) //За текущий месяц
            {
                transactions = transactions.Where(t => t.TransDate.Month == DateTime.Now.Month);
            }
            
            int i=0;
            foreach (var cat in categories)
            {
                GraphItem item = new GraphItem();
                item.Amount = transactions.Where(t => t.CategoryId == cat.Id).ToList().Sum(s => s.Amount);
                if (item.Amount > 0)
                {
                    item.Caption = cat.Name;
                    item.Color = arrColors[i];
                    GraphDataList.Add(item);
                    i++;
                }
            }
        }

        //Строка значений
        public string AmountsString()
        {
            string sumStr = "";
            foreach (var item in GraphDataList)
            {
                sumStr += item.Amount.ToString() + ",";
            }
            sumStr = sumStr.TrimEnd(',');
            return sumStr;
        }

        //Строка заголовков
        public string CaptionsString()
        {
            string sumStr = "";
            foreach (var item in GraphDataList)
            {
                sumStr += "'"+item.Caption + "',";
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
                sumStr += "'"+item.Color + "',";
            }
            sumStr = sumStr.TrimEnd(',');
            return sumStr;
        }


    }
}