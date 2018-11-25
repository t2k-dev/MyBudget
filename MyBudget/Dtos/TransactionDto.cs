using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudget.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public DateTime TransDate { get; set; }

        public string CategoryName { get; set; }        

        public bool IsSpending { get; set; }        

        public bool IsPlaned { get; set; }

    }
}