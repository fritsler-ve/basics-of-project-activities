using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ConsoleApplication1
{
    internal class SolvePolynom
    {

        public static string SolvePolynomExp(string str)
        {
            var Parameters = ParsePolynome(str);
            var result = Solver(Parameters);
            if (result != "no roots")
            {
                result.Replace(',', '.');
            }

            return result;
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
        
        public static string Solver(List<double> Parameters)
        {
            double left = -1;
            double right = 1;
            var iterations = 0;
            var shift = 0.5;
            while (iterations < 1000) // ищем такие точки А и Б, что их значения отличаются знаком, ЛЮТЫЙ ГОВНОКОД
            {
                if (GetValue(Parameters, left + shift) * GetValue(Parameters, right + shift) < 0)
                {
                    left += shift;
                    right += shift;
                    break;
                }

                if (GetValue(Parameters, left - shift) * GetValue(Parameters, right + shift) < 0)
                {
                    left -= shift;
                    right += shift;
                    break;
                }
                
                if (GetValue(Parameters, left + shift) * GetValue(Parameters, right - shift) < 0)
                {
                    left += shift;
                    right -= shift;
                    break;
                }
                
                if (GetValue(Parameters, left - shift) * GetValue(Parameters, right - shift) < 0)
                {
                    left -= shift;
                    right -= shift;
                    break;
                }
                shift += 0.5;
                iterations++;
            }

            if (iterations == 1000)
            {
                return "no roots";
            }

            var middle = (left + right) / 2;
            
            /*
             * Ща у нас есть left и right, значения функций от которых отличаются знаком
             * В этой функции мы на каждом шаге полуаем последовательность отрезков,
             * у которых значения от правого и левого концов отличаются знаком
             * Последовательность стягивается, и сходится к нулю, йопта
             */
            while (Math.Abs(GetValue(Parameters, middle)) > 10e-7)
            {
                // Должно работать, хуй знает как на самом деле.
                if (GetValue(Parameters, right) * GetValue(Parameters, middle) > 0)
                {
                    right = middle;
                }
                else
                {
                    left = middle;
                }

                middle = (left + right) / 2;
            }
            return middle + "";
        }

        // Считает значение полинома от нужного корня.
        public static double GetValue(List<double> Parameters, double x)
        {
            var result = 0.0;
            foreach (var i in Parameters)
            {
                result = result * x + i;
            }

            return result;
        }
    }
}