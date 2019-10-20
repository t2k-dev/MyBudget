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

        public static string GeneratePassword()
        {
            string password = "";
            string[] arr = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Z", "b", "c", "d", "f", "g", "h", "j", "k", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z", "A", "E", "U", "Y", "a", "e", "i", "o", "u", "y" };
            Random rnd = new Random();
            for (int i = 0; i < 6; i++)
            {
                password = password + arr[rnd.Next(0, 57)];
            }
            return password;
        }
    }
}