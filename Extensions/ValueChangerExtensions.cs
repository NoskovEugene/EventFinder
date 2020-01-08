using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventFinder.Extensions
{
    public static class ValueChangerExtensions
    {
        public static string ChangeNum(this int num)
        {
            string result = null;
            
            return num < 10 ? result = "0" + num : result = num.ToString();
        }

        public static string ChangeString(this string value)
        {
            string result = null;

            return value != null ? result = value : result = "Нет записи";
        }
    }
}
