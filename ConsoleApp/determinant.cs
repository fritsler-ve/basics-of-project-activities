using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace DetermonantChallenge
{
    public class Determinant
    {
        public static string GetDeterminant(string text)
        {
            var strings = text.Split(@"\\");
            var matrix = new BigInteger[strings.Length][];
            for (int i = 0; i < strings.Length; i++)
            {
                matrix[i] = new BigInteger[strings.Length];
                var str = strings[i].Split('&');
                for (int j = 0; j < strings.Length; j++)
                {
                    str[j].Trim();
                    matrix[i][j] = BigInteger.Parse(str[j]);
                }
                Console.WriteLine();
            }
            var result = GetD(matrix);
            return result.ToString();
        }

        public static string GetInversed(string text)
        {
            var strings = text.Split(@"\\");
            var matrix = new double[strings.Length, strings[0].Split("&").Length];
            for (int i = 0; i < strings.Length; i++)
            {
                var str = strings[i].Split('&');
                for (int j = 0; j < strings.Length; j++)
                {
                    str[j].Trim();
                    matrix[i, j] = double.Parse(str[j]);
                }
            }

            var matrixBuilded = Matrix<double>.Build.DenseOfArray(matrix);
            matrixBuilded = matrixBuilded.Inverse();
            var result = new StringBuilder();

            for (var i = 0; i < matrixBuilded.RowCount; i++)
            {
                for (var j = 0; j < matrixBuilded.ColumnCount; j++)
                {
                    //Todo unsolvable   - NaN and Infinity
                    if (double.IsNaN(matrixBuilded[i, j]) || double.IsInfinity(matrixBuilded[i, j]))
                        return "unsolvable";

                    result.Append(matrixBuilded[i, j].ToString(CultureInfo.InvariantCulture));
                    if (j != matrixBuilded.ColumnCount - 1)
                        result.Append(" & ");
                }

                if (i != matrixBuilded.RowCount - 1)
                    result.Append(@" \\ ");
            }

            return result.ToString();
        }

        public static BigInteger GetD(BigInteger[][] matrix)
        {
            if (matrix.Length == 1)
                return matrix[0][0];
            var subMatrix = new BigInteger[matrix.Length - 1][];
            BigInteger result = 0;
            for (int i = 0; i < subMatrix.Length; i++)
                subMatrix[i] = new BigInteger[subMatrix.Length];
            for (int i = 0; i < matrix.Length; i++)
            {
                var jj = 0;
                for (int j = 0; j < subMatrix.Length; j++)
                {
                    if (j == i)
                    {
                        jj++;
                    }
                    for (int k = 0; k < subMatrix.Length; k++)
                    {
                        subMatrix[k][j] = matrix[k + 1][jj];
                    }
                    jj++;
                }
                result += (BigInteger)Math.Round(Math.Pow((-1), i)) * matrix[0][i] * GetD(subMatrix);
            }
            return result;
        }

    }
}
