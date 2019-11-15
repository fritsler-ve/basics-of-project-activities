using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using MathNet.Numerics;

namespace ConsoleCoreApp
{
    public class Polynome
    {
        public static string GetPolynomialRoot(string quest)
        {
            var coefs = ParsePolynome(quest).ToArray();
            var result = FindRoots.Polynomial(coefs);
            foreach (var x in result)
            {
                if (x.Imaginary == 0)
                    return x.Real.ToString(CultureInfo.InvariantCulture);
            }
            return "no roots";
        }

        public static List<double> ParsePolynome(string str)
        {
            var digits = str.Split('+');
            var result = new List<double>();
            var degree = GetDegree(digits[0]);
            foreach (var variable in digits)
            {
                var separatedParts = variable.Split('^');
                var newDegree = GetDegree(variable);
                while (newDegree != degree)
                {
                    result.Add(0);
                    degree--;
                }
                result.Add(FindNumber(separatedParts[0]));
                if (separatedParts[0].Contains("-"))
                    result[result.Count - 1] *= (-1);
                degree--;
            }
            while (degree != -1)
            {
                result.Add(0);
                degree--;
            }
            result.Reverse();
            return result;
        }

        public static double FindNumber(string str)
        {
            var coef = new StringBuilder();
            for (var i = 0; i < str.Length; i++)
            {
                if (char.IsDigit(str[i]) || str[i] == '.')
                    coef.Append(str[i]);
            }
            return (double.Parse(coef.ToString(), CultureInfo.InvariantCulture));
        }

        public static int GetDegree(string digits)
        {
            var degree = 0;
            var first = digits.Split('^');
            if (first.Length == 1)
            {
                if (first[0].Contains("x"))
                    degree = 1;
                else
                    degree = 0;
            }
            else
            {
                degree = (int)Math.Round(FindNumber(first[1]));
            }
            return degree;
        }
    }
}
