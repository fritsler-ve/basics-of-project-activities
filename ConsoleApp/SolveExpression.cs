using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class SolveExpression
    {
        public static double GetResult(string input)
        {
            var numStack = new Stack<int>();
            var postfixNotation = GetPostfix(input);
            char ch;
            int num1, num2, interAns;
            var num = "";

            for (var i = 0; i < postfixNotation.Length; i++)
            {
                ch = postfixNotation[i];

                if (char.IsDigit(ch))
                    num += ch;
                else if (ch == '|')
                {
                    if (num != "")
                        numStack.Push(int.Parse(num));
                    num = "";
                }
                else
                {
                    if (num != "")
                        numStack.Push(int.Parse(num));
                    num = "";
                    num2 = numStack.Pop();
                    num1 = numStack.Pop();
                    interAns = SolveTwo(num1, num2, ch);

                    numStack.Push(interAns);
                }

            }

            return numStack.Pop();
        }
        private static int SolveTwo(int first, int second, char operation)
        {
            if (operation == '+') return first + second;
            if (operation == '-') return first - second;
            if (operation == '*') return first * second;
            if (operation == '/') return first / second;
            return first % second;
        }

        public static string GetPostfix(string input)
        {
            var output = new StringBuilder();
            var operStack = new Stack<char>();
            var num = "";

            for (var i = 0; i < input.Length; i++)
            {
                var currentChar = input[i];

                if (char.IsDigit(input[i]))
                {
                    num += currentChar;
                    if (i == input.Length - 1)
                        output.Append(num);
                    continue;
                }

                switch (currentChar)
                {
                    case '+':
                    case '-':
                        if (num != "")
                        {
                            output.Append(num);
                            output.Append("|");
                        }
                        num = "";
                        GotOper(currentChar, 1, operStack, output);
                        break;
                    case '*':
                    case '/':
                    case '%':
                        if (num != "")
                        {
                            output.Append(num);
                            output.Append("|");
                        }
                        num = "";
                        GotOper(currentChar, 2, operStack, output);
                        break;
                    case '(':
                        if (num != "")
                        {
                            output.Append(num);
                            output.Append("|");
                        }
                        num = "";
                        operStack.Push(currentChar);
                        break;
                    case ')':
                        if (num != "")
                        {
                            output.Append(num);
                            output.Append("|");
                        }
                        num = "";
                        GotParen(currentChar, operStack, output);
                        break;
                    default:
                        output.Append(currentChar);
                        break;
                }
            }

            while (operStack.Count != 0)
            {
                output.Append(operStack.Pop());
            }

            return output.ToString();
        }

        public static void GotOper(char opThis, int priority, Stack<char> stack, StringBuilder output)
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
                        output.Append(opTop);
                }
            }
            stack.Push(opThis);
        }

        public static void GotParen(char ch, Stack<char> stack, StringBuilder output)
        {
            while (stack.Count != 0)
            {
                char chx = stack.Pop();

                if (chx == '(')
                    break;
                else
                    output.Append(chx);
            }
        }
    }
}
