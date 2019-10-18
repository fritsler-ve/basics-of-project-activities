using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class SolveExpress
    {

        private static bool Priority(char input)
        {
            var prioritized = new List<char>() { '*', '/', '%' };
            return prioritized.Contains(input);
        }

        public static string ParseAndGiveAnswer(string data)
        {
            var parsedData = new List<string>();
            var currentNumber = "";
            for (var i = 0; i < data.Length; i++)
            {
                if (Char.IsDigit(data[i]))
                {
                    currentNumber += data[i];
                }
                else
                {
                    parsedData.Add(currentNumber);
                    currentNumber = "";
                    parsedData.Add(data[i] + "");
                }
            }

            // Добавил  
            parsedData.Add(currentNumber);

            return SolveExpression(ref parsedData);
        }

        private static string SolveExpression(ref List<string> data)
        {
            var usedElements = new List<bool>();
            for (int i = 0; i < data.Count; i++)
            {
                usedElements.Add(false);
            }

            char temp;
            for (var i = 1; i < data.Count - 1; i++) // ZALOOPA
            {
                temp = data[i][0];
                if (char.IsDigit(temp) || !Priority(temp))
                {
                    continue;
                }
                usedElements[i - 1] = true;
                usedElements[i] = true;
                data[i + 1] = "" + SolveTwo(int.Parse(data[i - 1]), int.Parse(data[i + 1]), temp);
            }

            var length = data.Count;
            var indexOfList = 0;
            //Удаляет в массиве использованные элементы
            while (true)
            {
                if (usedElements[indexOfList])
                {
                    usedElements.RemoveAt(indexOfList);
                    data.RemoveAt(indexOfList);
                    if (indexOfList != 0)
                        indexOfList--;
                }
                else
                    indexOfList++;

                if (indexOfList + 1 >= data.Count)
                    break;
            }

            for (var i = 1; i < data.Count - 1; i++)
            {
                temp = data[i].Length > 1 ? data[i][1] : data[i][0];
                if (char.IsDigit(temp))
                {
                    continue;
                }
                usedElements[i - 1] = true;
                usedElements[i] = true;
                data[i + 1] = "" + SolveTwo(int.Parse(data[i - 1]), int.Parse(data[i + 1]), temp);
            }
            return data[data.Count - 1];
        }

        private static int SolveTwo(int first, int second, char operation)
        {
            if (operation == '+') return first + second;
            if (operation == '-') return first - second;
            if (operation == '*') return first * second;
            if (operation == '/') return first / second;
            return first % second;
        }
    }
}
