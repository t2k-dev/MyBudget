﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudget.Models.ApiDTOs.Goals
{
    public class GoalsListItemDTO
    {
        public int Id { get; set; }

        public string GoalName { get; set; }

        public byte Type { get; set; }

        public double Amount { get; set; }

        public double CurAmount { get; set; }

        public bool IsActive { get; set; }

        public string UserId { get; set; }

        public string CompleteDate { get; set; }
    }
}