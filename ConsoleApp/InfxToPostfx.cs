using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class InfxToPostfx
    {
        private static Stack<char> stack;
        private static string input;
        private static string output = "";
        public InfxToPostfx(string expression)
        {
            input = expression;
            stack = new Stack<char>();
        }

        public string GetPostfix()
        {
            var num = "";
            for (var i = 0; i < input.Length; i++)
            {
                var currentChar = input[i];

                if (char.IsDigit(input[i]))
                {
                    num += currentChar;
                    if (i == input.Length - 1)
                        output += num;
                    continue;
                }

                switch (currentChar)
                {
                    case '+':
                    case '-':
                        if (num != "")
                            output += num + "|";
                        num = "";
                        gotOper(currentChar, 1);
                        break;
                    case '*':
                    case '/':
                    case '%':
                        if (num != "")
                            output += num + "|";
                        num = "";
                        gotOper(currentChar, 2);
                        break;
                    case '(':
                        if (num != "")
                            output += num + "|";
                        num = "";
                        stack.Push(currentChar);
                        break;
                    case ')':
                        if (num != "")
                            output += num + "|";
                        num = "";
                        GotParen(currentChar);
                        break;
                    default:
                        output += currentChar;
                        break;
                }
            }

            while (stack.Count != 0)
            {
                output += stack.Pop();
            }

            return output;
        }

        public static void gotOper(char opThis, int priority)
        {
            while (stack.Count != 0)
            {
                char opTop = stack.Pop();
                if (opTop == '(')
                {
                    stack.Push(opTop);
                    break;
                }
                else
                {
                    int priorityOfNewOper;

                    if (opTop == '+' || opTop == '-')
                        priorityOfNewOper = 1;
                    else
                        priorityOfNewOper = 2;

                    if (priorityOfNewOper < priority)
                    {
                        stack.Push(opTop);
                        break;
                    }
                    else
                        output += opTop;
                }
            }
            stack.Push(opThis);
        }

        public static void GotParen(char ch)
        {
            while (stack.Count != 0)
            {
                char chx = stack.Pop();

                if (chx == '(')
                    break;
                else
                    output += chx;
            }
        }
    }
}