using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    internal class SolvePolynom
    {
        public static string SolvePolynomExp(List<double> Parameters)
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