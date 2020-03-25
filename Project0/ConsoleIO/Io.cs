using System.Collections.Generic;
using System;
namespace Project0.ConsoleUI.ConsoleIO
{
    public class Io: IInputter, IOutputter
    {
        public void Output(string str)
        {
            Console.Write(str);
        }
        public string Input()
        {
            return Console.ReadLine().ToLower();
        }
    }
}