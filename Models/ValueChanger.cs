using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventFinder.Models
{
    public class ValueChanger
    {
        public static string ChangeNum(int num)
        {
            string result = null;
            if (num < 10)
            {
                result = "0" + num;
            }
            else
            {
                result = num.ToString();
            }
            return result;
        }
    }
}
