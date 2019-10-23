using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class SolvePostfxExp
    {
        private string input;
        private Stack<int> stack;
        public SolvePostfxExp(string expression)
        {
            input = expression;
        }

        public double GetResult()
        {
            stack = new Stack<int>();
            char ch;
            int num1, num2, interAns;
            var num = "";

            for (var i = 0; i < input.Length; i++)
            {
                ch = input[i];

                if (char.IsDigit(ch))
                    num += ch;
                else if (ch == '|')
                {
                    if (num != "")
                        stack.Push(int.Parse(num));
                    num = "";
                }
                else
                {
                    if (num != "")
                        stack.Push(int.Parse(num));
                    num = "";
                    num2 = stack.Pop();
                    num1 = stack.Pop();
                    interAns = SolveTwo(num1, num2, ch);

                    stack.Push(interAns);
                }

            }

            return stack.Pop();
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