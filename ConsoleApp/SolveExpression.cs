using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class SolveExpression
    {
        public static string GetResult(string input)
        {
            var numStack = new Stack<string>();
            var postfixNotation = GetPostfix(input);
            char ch;
            string num1, num2, interAns;
            var num = "";

            for (var i = 0; i < postfixNotation.Length; i++)
            {
                ch = postfixNotation[i];

                if (char.IsDigit(ch) || ch == 'i')
                    num += ch;
                else if (ch == '|')
                {
                    if (num != "")
                        numStack.Push(num);
                    num = "";
                }
                else
                {
                    var foundComplex = false;
                    var result = "";

                    if (num != "")
                        numStack.Push(num);
                    num = "";
                    num2 = numStack.Pop();
                    if (numStack.Count == 0)
                        return num2;
                    num1 = numStack.Pop();
                    if (!num1.Contains("i") && !num2.Contains("i"))
                    {
                        result = SolveTwo(int.Parse(num1), int.Parse(num2), ch) + "";
                        numStack.Push(result);
                        continue;
                    }

                    /*** Complex part ***/
                    // If there is already record of complex num and 
                    // usual num e.g. 2+3i
                    if (num1.Contains("+") || num1.Contains("-") || num2.Contains("+") || num2.Contains("-"))
                    {
                        var tmpOpers = new char[] { '+', '-' };
                        var num1Length = GetAmountNumsInExpression(num1);
                        var num2Length = GetAmountNumsInExpression(num2);

                        if (num2Length > num1Length)
                            result = GetOperOnComplexNum(num1, ch, num2);
                        if (num2Length < num1Length)
                            result = GetOperOnComplexNum(num2, ch, num1);

                        if (num2Length == num1Length)
                        {
                            var nums = num2.Split(new char[] { '+', '-' });
                            result = GetOperOnComplexNum(nums[0], ch, num1);
                            result = GetOperOnComplexNum(nums[1], ch, result);
                        }

                        if (result == "")
                            continue;
                        numStack.Push(result);
                        continue;
                    }
                    //Both numbers are complex
                    if (num2.Contains("i") && num1.Contains("i"))
                    {
                        num2 = num2.Substring(0, num2.Length - 1);
                        num1 = num1.Substring(0, num1.Length - 1);

                        result = SolveTwo(int.Parse(num1), int.Parse(num2), ch) + "i";
                        if (result == "0i")
                            continue;
                    }

                    // One of the numbers is complex and operations are 
                    // multiply, mod or dividing
                    if (num2.Contains("i")
                        && (ch == '*' || ch == '/' || ch == '%'))
                    {
                        num2 = num2.Substring(0, num2.Length - 1);
                        result = SolveTwo(int.Parse(num1), int.Parse(num2), ch) + "i";
                    }

                    if (num1.Contains("i")
                        && (ch == '*' || ch == '/' || ch == '%'))
                    {
                        num1 = num1.Substring(0, num1.Length - 1);
                        result = SolveTwo(int.Parse(num1), int.Parse(num2), ch) + "i";
                    }

                    // One of the numbers is complex and operations are
                    // plus and minus
                    if ((num2.Contains("i") || num1.Contains("i"))
                        && (ch == '+' || ch == '-'))
                    {
                        result = num1 + "" + ch + num2;
                    }

                    //Result
                    numStack.Push(result);
                }

            }

            return numStack.Pop();
        }

        static int GetAmountNumsInExpression(string expression)
        {
            var operations = new char[] { '+', '-' };
            var nums = expression.Split(operations);
            var counter = 0;

            for(var i = 0; i < nums.Length; i++)
            {
                if (nums[i] == "")
                    continue;
                else
                    counter++;
            }

            return counter;
        }

        static string GetOperOnComplexNum(string num, char oper, string complexNum)
        {
            var num1 = complexNum;
            var num2 = num;
            var ch = oper;
            var result = "";

            if (num1.Contains("+") || num1.Contains("-"))
            {
                // Get all pieces of previous result
                var operation = '+';

                if (num1.Contains("+"))
                    operation = '+';
                else
                    operation = '-';

                var nums = num1.Split(operation);
                nums[1] = operation + nums[1];

                // If you plus or minus with complex combination
                // e.g. (2+3i)-2i, (2+3i)-2
                if (ch == '+' || ch == '-')
                {
                    // If new number is complex
                    if (num2.Contains("i"))
                    {
                        num2 = num2.Substring(0, num2.Length - 1);

                        //If first number in previous result is complex
                        if (nums[0].Contains("i"))
                        {
                            nums[0] = nums[0].Substring(0, nums[0].Length - 1);
                            nums[0] = SolveTwo(int.Parse(nums[0]), int.Parse(num2), ch) + "i";
                            if (nums[0] == "0i")
                                nums[0] = "";
                        }

                        // If second number in previous result is complex
                        if (nums[1].Contains("i"))
                        {
                            nums[1] = nums[1].Substring(0, nums[1].Length - 1);
                            nums[1] = SolveTwo(int.Parse(nums[1]), int.Parse(num2), ch) + "i";
                            if (nums[1] == "0i")
                                nums[1] = "";
                        }

                        result = nums[0] + nums[1];
                    }
                    else
                    {
                        //If first number in previous result is usual
                        if (!nums[0].Contains("i"))
                            nums[0] = SolveTwo(int.Parse(nums[0]), int.Parse(num2), ch) + "";

                        // If second number in previous result is usual
                        if (!nums[1].Contains("i"))
                        {
                            nums[1] = SolveTwo(int.Parse(nums[1]), int.Parse(num2), ch) + "";
                            if (int.Parse(nums[1]) > 0)
                                nums[1] = "+" + nums[1];
                        }

                        result = nums[0] + nums[1];
                    }
                }
                // If you multiply, divide or mod with complex combination
                // e.g. (2+3i)*2i, (2+3i)*2
                else
                {
                    if (num2.Contains("i"))
                    {
                        num2 = num2.Substring(0, num2.Length - 1);

                        if (nums[0].Contains("i"))
                        {
                            nums[0] = nums[0].Substring(0, nums[0].Length - 1);
                            nums[0] = (SolveTwo(int.Parse(nums[0]), int.Parse(num2), ch) * -1) + "";
                            nums[1] = SolveTwo(int.Parse(nums[1]), int.Parse(num2), ch) + "i";
                        }

                        if (nums[1].Contains("i"))
                        {
                            nums[1] = nums[1].Substring(0, nums[1].Length - 1);
                            nums[1] = (SolveTwo(int.Parse(nums[1]), int.Parse(num2), ch) * -1) + "";
                            nums[0] = SolveTwo(int.Parse(nums[0]), int.Parse(num2), ch) + "i";
                        }

                        result = nums[0] + nums[1];
                    }
                    else
                    {
                        if (nums[0].Contains("i"))
                        {
                            nums[0] = nums[0].Substring(0, nums[0].Length - 1);
                            nums[0] = SolveTwo(int.Parse(nums[0]), int.Parse(num2), ch) + "i";
                            nums[1] = SolveTwo(int.Parse(nums[1]), int.Parse(num2), ch) + "";
                        }

                        if (nums[1].Contains("i"))
                        {
                            nums[1] = nums[1].Substring(0, nums[1].Length - 1);
                            nums[1] = SolveTwo(int.Parse(nums[1]), int.Parse(num2), ch) + "i";
                            nums[0] = SolveTwo(int.Parse(nums[0]), int.Parse(num2), ch) + "";
                        }

                        result = nums[0] + nums[1];
                    }
                }
            }

            return result;
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

                if (char.IsDigit(input[i]) || input[i] == 'i')
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
