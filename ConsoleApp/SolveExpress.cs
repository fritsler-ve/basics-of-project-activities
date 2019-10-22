using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class SolveExpress
    {
        private string input;
        private Stack<double> stack;
        public SolveExpress(string expression)
        {
            input = expression;
        }

        public double GetResult()
        {
            stack = new Stack<double>();
            char ch;
            double num1, num2, interAns;
            var num = "";

            for (var i = 0; i < input.Length; i++)
            {
                ch = input[i];

                if (char.IsDigit(ch))
                    num += ch;
                else if (ch == '|')
                {
                    if (num != "")
                        stack.Push(double.Parse(num));
                    num = "";
                }
                else
                {
                    if (num != "")
                        stack.Push(double.Parse(num));
                    num = "";
                    num2 = stack.Pop();
                    num1 = stack.Pop();
                    interAns = SolveTwo(num1, num2, ch);

                    stack.Push(interAns);
                }

            }

            return stack.Pop();
        }

        private static double SolveTwo(double first, double second, char operation)
        {
            if (operation == '+') return first + second;
            if (operation == '-') return first - second;
            if (operation == '*') return first * second;
            if (operation == '/') return first / second;
            return first % second;
        }
    }
}