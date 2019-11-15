using System;
using System.Collections.Generic;
using System.Text;

namespace Cum
{
    public class Caesar
    {
        public static string GetCypher(string str)
        {
            var a = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsDigit(str[i]) || str[i] == '-')
                    a.Append(str[i]);
                if (str[i] == '|')
                    break;
            }
            var bookves = "abcdefghijklmnopqrstuvwxyz0123456789' ";
            //var step = int.Parse(a.ToString());
            var charArr = new char[bookves.Length];

            for (int i = 0; i < bookves.Length; i++)
                charArr[i] = bookves[i];

            var charDict = new Dictionary<char, int>();
            for (int i = 0; i < charArr.Length; i++)
                charDict.Add(charArr[i], i);

            var result = new StringBuilder();
            var shit = str.Split('|')[1];
            for (var step = 0; step <= bookves.Length; step++)
            {
                for (int i = 0; i < shit.Length; i++)
                    result.Append(charArr[(charDict[shit[i]] + bookves.Length - step) % bookves.Length]);
                Console.WriteLine(result.ToString());
                result.Clear();
            }

            return result.ToString();
        }
    }
}