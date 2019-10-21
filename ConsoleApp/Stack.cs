using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class Stack
    {
        private static List<char> stackArray;
        private static int top;

        public Stack()
        {
            stackArray = new List<char>();
            top = -1;
        }
        public void Push(char symbol)
        {
            stackArray.Add(symbol);
            ++top;
        }

        public char Pop()
        {
            return stackArray[top--];
        }

        public char Peek()
        {
            return stackArray[top];
        }

        public int GetSize()
        {
            return top + 1;
        }

        public char PeekN(int n)
        {
            return stackArray[n];
        }

        public bool IsEmpty()
        {
            return top == -1;
        }
    }
}
