using Challenge;
using Challenge.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class Program
    {
        // ХУЛИ СМОТРИШЬ БЛЯДЬ!?

        public static bool IsZero(double number)
        {
            return System.Math.Abs(number) <= 10e-6;
        }
        public static string SolvePolynom(double first, double second, double third)
        {
            if (IsZero(first))
            {
                if (IsZero(second))
                {
                    if (IsZero(third))
                        return "1";
                    return "no roots";
                }
                else
                {
                    return (-third) / second + "";
                }
            }
            else
            {
                var discriminant = second * second - 4 * first * third;
                if (discriminant < 0)
                {
                    return "no roots";
                }
                else
                {
                    return (System.Math.Sqrt(discriminant) - second) / (2 * first) + "";
                }
            }
        }
        static string Parser(string text)
        {
            var coefs = new double[3];
            var shit = text.Split('+');

            foreach (var s in shit)
            {
                var power = 2;
                var number = new StringBuilder();
                for (int i = 0; i < s.Length; i++)
                {
                    if (char.IsLetter(s[i]))
                    {
                        if (i == s.Length || s[i + 1] != '^')
                            power = 1;
                        else power = 0;
                        break;
                    }

                    if (char.IsDigit(s[i]) || s[i] == '.' || s[i] == '-')
                    {
                        number.Append(s[i]);
                    }
                }
                var numberStr = number.ToString();

                if (string.IsNullOrEmpty(numberStr)) coefs[power] = 1;
                else if (numberStr == "-") coefs[power] = -1;
                else coefs[power] = double.Parse(numberStr, System.Globalization.CultureInfo.InvariantCulture);
            }
            return SolvePolynom(coefs[0], coefs[1], coefs[2]).Replace(',', '.');
        }

        static void Main(string[] args)
        {
            const string teamSecret = "jSVy9hRtt7bpflchqLGSc3l0iEgaRtp";
            var test = new InfxToPostfx("((8+8)-(3-12%11-3)+(1+11)-(12*3-0))+((12+3)+(6+8)-(4-9%7))+((13/3)%1)");
            var solve = new SolvePostfxExp(test.GetPostfix());
            Console.WriteLine(solve.GetResult());

            //var challengeClient = new ChallengeClient(teamSecret);
            /* 
            const string challengeId = "projects-course";
            Console.WriteLine($"Нажми ВВОД, чтобы получить информацию о челлендже {challengeId}");
            Console.ReadLine();
            var challenge = challengeClient.GetChallengeAsync(challengeId).Result;
            Console.WriteLine(challenge.Description);
            Console.WriteLine();
            Console.WriteLine("----------------");
            Console.WriteLine();

            Console.WriteLine($"Нажми ВВОД, чтобы получить список взятых командой задач");
            Console.ReadLine();
            var allTasks = challengeClient.GetAllTasksAsync().Result;
            //for (int i = 0; i < allTasks.Count; i++)  Console.WriteLine(allTasks[i].Question);

            for (int i = 0; i < allTasks.Count; i++)
            {
                var task = allTasks[i];
                Console.WriteLine($"  Задание {i + 1}, статус {task.Status}");
                Console.WriteLine($"                {task.Question}");
                Console.WriteLine($"                {task.TeamAnswer}");
            }

            Console.WriteLine("----------------");
            Console.WriteLine();

            const string type = "polynomial-root";

            while (true)
            {
                string round = challenge.Rounds[1].Id;
                Console.WriteLine($"Нажми ВВОД, чтобы получить задачу типа {type}");
                //Console.ReadLine();
                var newTask = challengeClient.AskNewTaskAsync(round, type).Result;
                Console.WriteLine($"  Новое задание, статус {newTask.Status}");
                Console.WriteLine($"  Формулировка: {newTask.UserHint}");
                Console.WriteLine($"                {newTask.Question}");
                Console.WriteLine();
                Console.WriteLine("----------------");
                Console.WriteLine();

                string answer = Parser(newTask.Question) + "";

                Console.WriteLine($"Нажми ВВОД, чтобы ответить на полученную задачу самым правильным ответом: {answer}");
                Console.ReadLine();

                var updatedTask = challengeClient.CheckTaskAnswerAsync(newTask.Id, answer).Result;
                Console.WriteLine($"  Новое задание, статус {updatedTask.Status}");
                Console.WriteLine($"  Формулировка:  {updatedTask.UserHint}");
                Console.WriteLine($"                 {updatedTask.Question}");
                updatedTask.TeamAnswer = Parser(updatedTask.Question) + "";
                Console.WriteLine($"  Ответ команды: {Parser(updatedTask.Question)}");
                Console.WriteLine();
                if (updatedTask.Status == TaskStatus.Success)
                    Console.WriteLine($"Ура! Ответ угадан!");
                else if (updatedTask.Status == TaskStatus.Failed)
                    Console.WriteLine($"Похоже ответ не подошел и задачу больше сдать нельзя...");
                Console.WriteLine();
                Console.ReadLine();

            }
            Console.WriteLine("----------------");
            Console.WriteLine();

            Console.WriteLine($"Нажми ВВОД, чтобы завершить работу программы");
            Console.ReadLine();
            */
        }
    }
}