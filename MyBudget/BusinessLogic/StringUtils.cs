using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudget.BusinessLogic
{
    public static class StringUtils
    {
        /// <summary>
        /// Returns null if there are only spaces in text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string DeleteSpaces(string text)
        {
            if(String.IsNullOrWhiteSpace(text))
            {
                return null;
            };
            return text;
        }
    }
}