using Challenge;
using Challenge.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using DetermonantChallenge;
using Cum;
using ConsoleCoreApp;
using Newtonsoft.Json;
using System.Xml;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        static string LoadPage(string url)
        {
            var result = "";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json; charset=utf-8";
            var response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var receiveStream = response.GetResponseStream();
                if (receiveStream != null)
                {
                    StreamReader readStream;
                    if (response.CharacterSet == null)
                        readStream = new StreamReader(receiveStream);
                    else
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    result = readStream.ReadToEnd();
                    readStream.Close();
                }
                response.Close();
            }

            return result;
        }

        static string ConvertToWolframFormat(string expression)
        {
            var result = expression;
            result.Replace("(", "%28");
            result.Replace(")", "%29");
            result.Replace("+", "%2B");
            result.Replace(":", "%2F");

            return result;
        }
        static void Main(string[] args)
        {
            /*
             * 
             * %28 - (
             * %29 - )
             * %2B - +
             * %2F - :
             * 
             * */
            //var text = "first longest word=something|80dbnjn hdijn pfjn00ijtdfn hdkjn tnbsj6 tnbsj088jb30urspjpyprf0dpjkpnsjs3trp8j6 pdjs0zpn hdkjvhipjn hs";
            //Console.WriteLine(Visioner.GetCypher(text));

            /*
            var expresion = "2/2";
            var url = "http://api.wolframalpha.com/v2/query?appid=PYVXLP-Y5W998K993&input=" + expresion;
            WebClient webClient = new WebClient();
            string result = webClient.DownloadString(url);
            Console.WriteLine(result);
            */
            //var dict = new Dictionary<string, string>();
            //var json = @"{""padlocked"":-1,""myotonias"":-9,""stanchly"":-8,""millwheel"":2,""denizenation"":9,""pointswoman"":{""linework"":-3,""sizzling"":-5,""combating"":3,""underlout"":5},""youths"":{""ginglymodian"":-3,""embossage"":5,""subperiosteal"":6,""seastroke"":-9,""indurative"":-5,""composition"":-9}}";
            /*var obj = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            var summ = 0.0;
            foreach(var e in obj)
            {
                summ += double.Parse(e.Value);
            }
            Console.WriteLine((int)summ);
            */
            //var info = JsonConvert.DeserializeObject<Info>(json);
            //var text = File.ReadAllText("text.txt");
            //Console.WriteLine(Determinant.GetDeterminant(@"9 & 0 & -8 & -7 & 8 & 7 & 7 & -8 \\ 4 & 4 & 3 & 9 & 9 & 8 & -1 & -3 \\ -11 & -1 & 9 & 4 & -5 & -3 & -10 & -11 \\ -7 & 0 & -11 & -6 & 1 & -4 & -1 & 10 \\ 2 & -10 & 2 & -10 & 1 & 0 & -7 & -8 \\ -10 & 6 & 1 & 10 & -8 & 8 & 5 & 7 \\ 3 & -8 & 5 & -10 & -9 & 9 & -11 & 1 \\ -2 & -9 & -11 & -3 & 7 & 1 & 1 & -4"));
            /*
            var expression = "(5*6i)-(3*8+4i+8)-(6i+4-5-6i)";
            string url = "https://www.wolframalpha.com/input/?i=2+%2B+3";
            var web = new HtmlAgilityPack.HtmlWeb();
            HtmlDocument doc = web.Load(url);

            /*var pageHtml = LoadPage("https://www.wolframalpha.com/input/?i=" + expression);
            var document = new HtmlDocument();
            document.LoadHtml(pageHtml);
            */
            /*
             * */
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
            /*
            var allTasks = challengeClient.GetAllTasksAsync().Result;

            for (int i = 0; i < allTasks.Count; i++)
            {
                var task = allTasks[i];
                Console.WriteLine($"  Задание {i + 1}, статус {task.Status}");
                Console.WriteLine($"                {task.Question}");
                Console.WriteLine($"                {task.TeamAnswer}");
            }
            */
            Console.WriteLine("----------------");
            Console.WriteLine();
            while (true)
            {
                Console.WriteLine("          ");
                string round = challenge.Rounds[2].Id;
                //Console.ReadLine();
                var newTask = challengeClient.AskNewTaskAsync(round).Result;
                Console.WriteLine($"  Новое задание, статус {newTask.Status}");
                Console.WriteLine($"  Формулировка: {newTask.UserHint}");
                Console.WriteLine($"                {newTask.Question}");
                Console.WriteLine();
                Console.WriteLine("----------------");

                var answer = "";

                if (newTask.TypeId == "math")
                {
                    if (newTask.Question.Contains("i") && 
                        (newTask.Question.Contains("/") || newTask.Question.Contains("%")))
                        continue;

                    if (newTask.Question.Contains("i"))
                    {

                        var expresion = newTask.Question.Replace("+", "%2B");
                        var url = "http://api.wolframalpha.com/v2/query?appid=PYVXLP-Y5W998K993&input=" + expresion + "&includepodid=Result&format=plaintext";
                        WebClient webClient = new WebClient();
                        string result = webClient.DownloadString(url);
                        XmlDocument xDoc = new XmlDocument();
                        xDoc.LoadXml(result);
                        XmlElement xRoot = xDoc.DocumentElement;
                        XmlNodeList elements = xDoc.SelectNodes("//pod");

                        foreach (XmlElement element in elements)
                            answer = element.InnerText.Replace(" ", "");
                    }
                    else
                        answer = SolveExpression.GetResult(newTask.Question);
                }

                if (newTask.TypeId == "determinant")
                    answer = Determinant.GetDeterminant(newTask.Question);

                if (newTask.TypeId == "polynomial-root")
                    answer = Polynome.GetPolynomialRoot(newTask.Question);

                if (newTask.TypeId == "cypher")
                    continue;

                if (newTask.TypeId == "shape")
                    answer = "rectangle";

                if (newTask.TypeId == "string-number")
                    continue;

                if (newTask.TypeId == "inverse-matrix")
                    answer = Determinant.GetInversed(newTask.Question);

                if (newTask.TypeId == "json")
                    answer = JsonParser.SosiBibu(newTask.Question);
                Console.WriteLine(answer);

                // Ответ
                var updatedTask = challengeClient.CheckTaskAnswerAsync(newTask.Id, answer).Result;
                Console.WriteLine($"  Новое задание, статус {updatedTask.Status}");
                Console.WriteLine($"  Формулировка:  {updatedTask.UserHint}");
                Console.WriteLine($"                 {updatedTask.Question}");
                Console.WriteLine();
                if (updatedTask.Status == TaskStatus.Success)
                    Console.WriteLine($"Ура! Ответ угадан!");
                else if (updatedTask.Status == TaskStatus.Failed)
                    Console.WriteLine($"Похоже ответ не подошел и задачу больше сдать нельзя...");
                Console.WriteLine("-------------------------");
            }
            /*
            string answer = newTask.Question + "";

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
            */

            Console.WriteLine("----------------");
            Console.WriteLine();

            Console.WriteLine($"Нажми ВВОД, чтобы завершить работу программы");
            Console.ReadLine();
        }
    }
}