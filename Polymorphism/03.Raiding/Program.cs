﻿using _03.Raiding.Core;
using _03.Raiding.Core.Interfaces;

namespace _03.Raiding
{
    public class Program
    {
        static void Main(string[] args)
        {
            IEngine engine = new Engine();

            engine.Run();
        }
    }
}