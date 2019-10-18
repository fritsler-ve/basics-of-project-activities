namespace ConsoleApp
{
    class SolvePolynoms
    {
        public static double Parser(string input)
        {
            var rows = input.Split(@" \\ ");
            var arr = new double[3, 3];

            for (var row = 0; row < arr.GetLength(1); row++)
            {
                var splittedRow = rows[row].Split(" & ");

                for (var column = 0; column < arr.GetLength(1); column++)
                    arr[row, column] = double.Parse(splittedRow[column]);
            }

            return arr[0, 0] * (arr[1, 1] * arr[2, 2] - arr[2, 1] * arr[1, 2])
                - arr[0, 1] * (arr[1, 0] * arr[2, 2] - arr[2, 0] * arr[1, 2])
                + arr[0, 2] * (arr[1, 0] * arr[2, 1] - arr[1, 1] * arr[2, 0]);
        }
    }
}
