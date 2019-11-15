using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    public class JsonParser
    {
        public static string SosiBibu(string input)
        {
            var isNumber = false;
            var number = new StringBuilder();
            var result = 0;

            for(var i = 1; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]))
                {
                    isNumber = true;
                    if (input[i - 1] == '-')
                        number.Append("-");
                    number.Append(input[i]);
                }
                else
                {
                    if (isNumber)
                    {
                        result += int.Parse(number.ToString());
                        number.Clear();
                        isNumber = false;
                    }
                }

            }

            return result.ToString();
        }
    }
}
