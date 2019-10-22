using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class InfxToPostfx
    {
        private static Stack stack;
        private static string input;
        private static string output = "";
        public InfxToPostfx(string expression)
        {
            input = expression;
            stack = new Stack(expression.Length);
        }

        public string GetPostfix()
        {
            for (var i = 0; i < input.Length; i++)
            {
                var currentChar = input[i];
                switch (currentChar)
                {
                    case '+':
                    case '-':
                        gotOper(currentChar, 1);
                        break;
                    case '*':
                    case '/':
                    case '%':
                        gotOper(currentChar, 2);
                        break;
                    case '(':
                        stack.Push(currentChar);
                        break;
                    case ')':
                        GotParen(currentChar);
                        break;
                    default:
                        output += currentChar;
                        break;
                }
            }

            while (!stack.IsEmpty())
            {
                output += stack.Pop();
            }

            return output;
        }

        public static void gotOper(char opThis, int priority)
        {
            while (!stack.IsEmpty())
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
            while (!stack.IsEmpty())
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
