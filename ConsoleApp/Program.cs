using Challenge;
using Challenge.DataContracts;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        // ХУЛИ СМОТРИШЬ БЛЯДЬ!?

        private static bool Priority(char input)
        {
            var prioritized = new List<char>() {'*', '/', '%'};
            return prioritized.Contains(input);
        }
        
        private static string Parse(string data)
        {
            var parsedData = new List<string>();
            var currentNumber = "";
            for (var i = 0; i < data.Length; i++)
            {
                if (Char.IsDigit(data[i]))
                {
                    currentNumber += data[i];
                }
                else
                {
                    parsedData.Add(currentNumber);
                    currentNumber = "";
                    parsedData.Add(data[i] + "");
                }
            }

            // Добавил  
            parsedData.Add(currentNumber);

            return SolveExpression(ref parsedData);
        }

        private static string SolveExpression(ref List<string> data)
        {
            var usedElements = new List<bool>();
            for (int i = 0; i < data.Count; i++)
            {
                usedElements.Add(false);
            }
            
            char temp;
            for (var i = 1; i < data.Count - 1; i++) // ZALOOPA
            {
                temp = data[i][0];
                if (char.IsDigit(temp) || !Priority(temp))
                {
                    continue;
                }
                usedElements[i - 1] = true; 
                usedElements[i] = true; 
                data[i + 1] = "" + SolveTwo(int.Parse(data[i - 1]), int.Parse(data[i + 1]), temp);
            }

            var length = data.Count;
            var indexOfList = 0;
            //Удаляет в массиве использованные элементы
            while (true)
            {
                if (usedElements[indexOfList])
                {
                    usedElements.RemoveAt(indexOfList);
                    data.RemoveAt(indexOfList);
                    if (indexOfList != 0)
                        indexOfList--;
                }
                else
                    indexOfList++;

                if (indexOfList + 1 >= data.Count)
                    break;
            }
            /*
             * for (var i = 0; i < length; i++)
            {
                if (usedElements[])
                {
                    usedElements.RemoveAt(i);
                    data.RemoveAt(i);
                }
            }
            */
            
            for (var i = 1; i < data.Count - 1; i++) // может показаться что вылезет за границы массива, 
                                                 // и я не ебу, так ли это
            {
                temp = data[i].Length > 1 ? data[i][1] : data[i][0];
                if (char.IsDigit(temp))
                {
                    continue;
                }
                usedElements[i - 1] = true;
                usedElements[i] = true; 
                data[i + 1] = "" + SolveTwo(int.Parse(data[i - 1]), int.Parse(data[i + 1]), temp);
            }
            return data[data.Count - 1];
        }

        private static int SolveTwo(int first, int second, char operation)
        {
            if (operation == '+') return first + second;
            if (operation == '-') return first - second;
            if (operation == '*') return first * second;
            if (operation == '/') return first / second;
            return first % second;
        }

        static void Main(string[] args)
        {
            const string teamSecret = "jSVy9hRtt7bpflchqLGSc3l0iEgaRtp";
            var challengeClient = new ChallengeClient(teamSecret);

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
            for (int i = 0; i < allTasks.Count; i++)
            {
                allTasks[i].TeamAnswer = Parse(allTasks[i].Question);
            }

            for (int i = 0; i < allTasks.Count; i++)
            {
                var task = allTasks[i];
                Console.WriteLine($"  Задание {i + 1}, статус {task.Status}");
                Console.WriteLine($"                {task.Question}");
                Console.WriteLine($"                {task.TeamAnswer}");
            }

            Console.WriteLine("----------------");
            Console.WriteLine();

            const string type = "math";
            string round = challenge.Rounds[0].Id;
            Console.WriteLine($"Нажми ВВОД, чтобы получить задачу типа {type}");
            Console.ReadLine();
            var newTask = challengeClient.AskNewTaskAsync(round, type).Result;
            Console.WriteLine($"  Новое задание, статус {newTask.Status}");
            Console.WriteLine($"  Формулировка: {newTask.UserHint}");
            Console.WriteLine($"                {newTask.Question}");
            Console.WriteLine();
            Console.WriteLine("----------------");
            Console.WriteLine();

            string answer = Parse(newTask.Question) + "";
            
            Console.WriteLine($"Нажми ВВОД, чтобы ответить на полученную задачу самым правильным ответом: {answer}");
            Console.ReadLine();
            
            var updatedTask = challengeClient.CheckTaskAnswerAsync(newTask.Id, answer).Result;
            Console.WriteLine($"  Новое задание, статус {updatedTask.Status}");
            Console.WriteLine($"  Формулировка:  {updatedTask.UserHint}");
            Console.WriteLine($"                 {updatedTask.Question}");
            updatedTask.TeamAnswer = Parse(updatedTask.Question) + "";
            Console.WriteLine($"  Ответ команды: {Parse(updatedTask.Question)}");
            Console.WriteLine();
            if (updatedTask.Status == TaskStatus.Success)
                Console.WriteLine($"Ура! Ответ угадан!");
            else if (updatedTask.Status == TaskStatus.Failed)
                Console.WriteLine($"Похоже ответ не подошел и задачу больше сдать нельзя...");
            Console.WriteLine();
            Console.WriteLine("----------------");
            Console.WriteLine();

            Console.WriteLine($"Нажми ВВОД, чтобы завершить работу программы");
            Console.ReadLine();
        }
    }
}