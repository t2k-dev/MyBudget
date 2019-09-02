using MyBudget.Models.ApiDTOs.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudget.Models.ApiDTOs
{
    public class NewLoginResponseDTO
    {
        public class UserSettingsModel
        {
            public string UserId { get; set; }
            public string DefCurrency { get; set; }
            public DateTime? UpdateDate { get; set; }
            public bool CarryOverRests { get; set; }
            public bool UseTemplates { get; set; }            
        }

        public int Status { get; set; }
        public UserSettingsModel UserSettings { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public List<TransactionListItem> Transactions { get; set; }
        

    }
}