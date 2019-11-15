using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class Visioner
    {
        public static string GetCypher(string str)
        {
            var bookves = "abcdefghijklmnopqrstuvwxyz0123456789' ";
            //var step = int.Parse(a.ToString());
            var charArr = new char[bookves.Length];

            for (int i = 0; i < bookves.Length; i++)
                charArr[i] = bookves[i];

            var charDict = new Dictionary<char, int>();
            for (int i = 0; i < charArr.Length; i++)
                charDict.Add(charArr[i], i);

            var result = new StringBuilder();
            var keyword = str.Split('|')[0].Split("=")[1];
            var stepArr = new int[keyword.Length];

            for (var i = 0; i < stepArr.Length; i++)
            {
                stepArr[i] = charDict[keyword[i]];
            }

            var shit = str.Split('|')[1];
            var j = 0;

            for (var i = 0; i < shit.Length; i++)
            {
                var index = charDict[shit[i]];
                index -= stepArr[j];
                if (index < 0)
                    index += bookves.Length;

                result.Append(charArr[index]);

                j++;
                j = j % keyword.Length;
            }

            return result.ToString();
        }
    }
}
