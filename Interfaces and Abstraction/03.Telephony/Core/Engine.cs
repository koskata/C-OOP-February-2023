using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _03.Telephony.Models.Interfaces;
using _03.Telephony.Models;

namespace _03.Telephony.Core
{
    public class Engine : IEngine
    {
        public void Run()
        {
            string[] numbers = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string[] urls = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            ICallable caller;
            foreach (var number in numbers)
            {
                if (number.Length == 10)
                {
                    caller = new Smartphone();
                    try
                    {
                        Console.WriteLine(caller.Call(number));

                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (number.Length == 7)
                {
                    caller = new StationaryPhone();
                    try
                    {
                        Console.WriteLine(caller.Call(number));

                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
            }

            foreach (var url in urls)
            {
                IBrowsable browser = new Smartphone();

                try
                {
                    Console.WriteLine(browser.Browse(url));

                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
