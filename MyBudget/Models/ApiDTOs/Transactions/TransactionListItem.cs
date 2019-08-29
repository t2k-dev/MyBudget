using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudget.Models.ApiDTOs.Transactions
{
    public class TransactionListItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public DateTime TransDate { get; set; }
        
        public int CategoryId { get; set; }

        public bool IsSpending { get; set; }

        public string Description { get; set; }

        public bool IsPlaned { get; set; }

        public string UserId { get; set; }

    }
}