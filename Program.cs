﻿using System;
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
            string path = @"C:\Git Repositories\TuringMachine\data.txt"; // pasikeisk kur yra tekstas
            int startPosition = 0; //set default starting position
            int iteration = 0;
            string[] taskLine;
            string[] data = new string[4];
            char[] line;
            var tasksList = new List<Task>();

            //********************************************************* Data reading ********************************************
            using (FileStream fs = File.OpenRead(path)) // text reading copied from google
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    data = temp.GetString(b).Split("\r\n"); // \n -- unix(Linux, and OSX) \r\n - windows
                }

                startPosition = Int32.Parse(data[0]);
                line = data[1].ToCharArray();
                for (int i = 2; i < data.Length; i++)
                {
                    if (data[i].Length > 0 && data[i].Length < 12)
                    {
                        taskLine = data[i].Split(" ");
                        bool _positioIsNumber = int.TryParse(taskLine[0], out int _position);
                        bool _nextPositionIsNumber = int.TryParse(taskLine[4], out int _nextPosition);

                        if (!_nextPositionIsNumber)
                        {
                            _nextPosition = -1;
                        }
                        var _task = tasksList.FirstOrDefault(x => x.position == _position);

                        if (_task == null)
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
                        }
                        else
                        {
                            _task.tasks.Add(new Action
                            {
                                currentState = taskLine[1],
                                nextState = taskLine[2],
                                moveDirection = taskLine[3],
                                nextPosition = _nextPosition
                            });
                        }
                    }
                }
            }

            //********************************************** Turing Machine **********************************************
        }
    }
}
