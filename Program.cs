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
            string path = @"C:\Git Repositories\TuringMachine\data.txt"; // pasikeisk kur yra tekstas
            int startPosition = 0; //set default starting position
            string[] taskLine;
            string[] data = new string[4];
            char[] charLine;
            string[] stringLine;
            var rulesList = new List<Task>();

            //********************************************************* Data reading ********************************************
            using (FileStream fs = File.OpenRead(path)) // text reading copied from google
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    data = temp.GetString(b).Split("\r\n"); // \n -- unix(Linux, and OSX) \r\n - windows
                }

                Int32.TryParse(data[0], out startPosition);

                charLine = data[1].ToCharArray();
                stringLine = new string[data.Length];
                for (int i = 0; i < charLine.Length; i++)
                {
                    stringLine[i] = charLine[i].ToString();
                }

                for (int i = 2; i < data.Length; i++)
                {
                    if (data[i].Length > 0 && data[i].Length < 12)
                    {
                        taskLine = data[i].Split(" ");
                        bool _headStateIsNumber = int.TryParse(taskLine[0], out int _headState);
                        bool _nextHeadStateIsNumber = int.TryParse(taskLine[4], out int _nextHeadState);

                        if (!_nextHeadStateIsNumber)
                        {
                            _nextHeadState = -1;
                        }
                        var _task = rulesList.FirstOrDefault(x => x.headState == _headState);

                        if (_task == null)
                        {
                            var notExistingTask = new Task();
                            var taskAction = new Action();
                            notExistingTask.headState = _headState;
                            notExistingTask.tasks = new List<Action>(){
                                new Action {
                                    currentSymbol = taskLine[1],
                                    nextSymbol = taskLine[2],
                                    moveDirection = taskLine[3],
                                    nextHeadState = _nextHeadState
                                }
                            };
                            rulesList.Add(notExistingTask);
                        }
                        else
                        {
                            _task.tasks.Add(new Action
                            {
                                currentSymbol = taskLine[1],
                                nextSymbol = taskLine[2],
                                moveDirection = taskLine[3],
                                nextHeadState = _nextHeadState
                            });
                        }
                    }
                }
            }

            //********************************************** Turing Machine **********************************************

            int headPosition = startPosition - 1; // first element of array
            int headState = 0;

            while (headState != -1)
            {
                var ruleForHead = rulesList.FirstOrDefault(x => x.headState == headState);

                if (ruleForHead == null)
                    break;

                var headAction = ruleForHead.tasks.FirstOrDefault(x => x.currentSymbol == stringLine[headPosition]);

                if (headAction == null)
                    break;

                stringLine[headPosition] = headAction.nextSymbol;
                headState = headAction.nextHeadState;
                if (headAction.moveDirection == "R")
                    headPosition++;
                else if (headAction.moveDirection == "L")
                    headPosition--;
            }

            // ****************** result *******************
            Console.WriteLine(string.Join("", stringLine));
            Console.ReadLine();
        }
    }
}
