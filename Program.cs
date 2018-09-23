using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Turing_Machiene
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"data.txt";
            int startPosition = 0; //set default starting position
            int iteration = 0;
            string [] taskLine;
            string [] data = new string[4];
            char[] line;
            var tasksList = new List<Task>();

        using (FileStream fs = File.OpenRead(path)) // text reading copied from google
        {
            byte[] b = new byte[1024];
            UTF8Encoding temp = new UTF8Encoding(true);
            while (fs.Read(b,0,b.Length) > 0)
            {
                data = temp.GetString(b).Split("\n");
            }

            startPosition = Int32.Parse(data[0]);
                line = data[1].ToCharArray();
                for(int i = 2; i < data.Length; i++)
                {
                    if(data[i].Length > 0 && data[i].Length < 10)
                    {
                        taskLine = data[i].Split(" ");
                        int _position = int.Parse(taskLine[0]);
                        int _nextPosition = int.Parse(taskLine[4]);
                        var _task = tasksList.FirstOrDefault(x => x.position == _position);

                        if(_task == null)
                        {
                            var notExistingTask = new Task();
                            var taskAction = new Action();
                            notExistingTask.position = _position;
                            notExistingTask.tasks = new List<Action>(){
                                new Action {
                                    currentState = taskLine[1],
                                    nextState = taskLine[2],
                                    moveDirection = taskLine[3],
                                    nextPosition = _nextPosition
                                }
                            };
                            tasksList.Add(notExistingTask);
                        } else {
                            _task.tasks.Add(new Action {
                                currentState = taskLine[1],
                                nextState = taskLine[2],
                                moveDirection = taskLine[3],
                                nextPosition = _nextPosition
                            });
                        }
                    }
                }
        }
            Console.WriteLine("Hello World!");
        }
    }
}
