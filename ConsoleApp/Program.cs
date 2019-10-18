using Challenge;
using Challenge.DataContracts;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        // ХУЛИ СМОТРИШЬ БЛЯДЬ!?


        static void Main(string[] args)
        {
            Console.WriteLine(test);
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
                Console.WriteLine(allTasks[i].Question);

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
            /* 
            while (true)
            {
                string round = challenge.Rounds[0].Id;
                Console.WriteLine($"Нажми ВВОД, чтобы получить задачу типа {type}");
                //Console.ReadLine();
                var newTask = challengeClient.AskNewTaskAsync(round, type).Result;
                Console.WriteLine($"  Новое задание, статус {newTask.Status}");
                Console.WriteLine($"  Формулировка: {newTask.UserHint}");
                Console.WriteLine($"                {newTask.Question}");
                Console.WriteLine();
                Console.WriteLine("----------------");
                Console.WriteLine();

                string answer = SolveExpress.ParseAndGiveAnswer(newTask.Question) + "";

                Console.WriteLine($"Нажми ВВОД, чтобы ответить на полученную задачу самым правильным ответом: {answer}");
                Console.ReadLine();

                var updatedTask = challengeClient.CheckTaskAnswerAsync(newTask.Id, answer).Result;
                Console.WriteLine($"  Новое задание, статус {updatedTask.Status}");
                Console.WriteLine($"  Формулировка:  {updatedTask.UserHint}");
                Console.WriteLine($"                 {updatedTask.Question}");
                updatedTask.TeamAnswer = SolveExpress.ParseAndGiveAnswer(updatedTask.Question) + "";
                Console.WriteLine($"  Ответ команды: {SolveExpress.ParseAndGiveAnswer(updatedTask.Question)}");
                Console.WriteLine();
                if (updatedTask.Status == TaskStatus.Success)
                    Console.WriteLine($"Ура! Ответ угадан!");
                else if (updatedTask.Status == TaskStatus.Failed)
                    Console.WriteLine($"Похоже ответ не подошел и задачу больше сдать нельзя...");
                Console.WriteLine();

            }
            */
            Console.WriteLine("----------------");
            Console.WriteLine();

            Console.WriteLine($"Нажми ВВОД, чтобы завершить работу программы");
            Console.ReadLine();
        }
    }
}