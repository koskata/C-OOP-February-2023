using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _04.BorderControl.Core.Interfaces;
using _04.BorderControl.Models;
using _04.BorderControl.Models.Interfaces;

using _05.BirthdayCelebrations.Models;
using _05.BirthdayCelebrations.Models.Interfaces;

namespace _04.BorderControl.Core
{
    public class Engine : IEngine
    {
        public void Run()
        {
            Dictionary<string, string> dic = new();
            Dictionary<string, string> exceptions = new();

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] cmdArgs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string type = cmdArgs[0];

                if (type == "Citizen")
                {
                    ICitizen citizen = new Citizen(cmdArgs[1], int.Parse(cmdArgs[2]), cmdArgs[3], cmdArgs[4]);

                    string[] birthdate = cmdArgs[4].Split("/");

                    if (dic.ContainsKey(cmdArgs[4]))
                    {
                        exceptions.Add(cmdArgs[4], birthdate[2]);
                    }
                    else
                    {
                        dic.Add(cmdArgs[4], birthdate[2]);

                    }
                }
                else if (type == "Pet")
                {
                    IPet pet = new Pet(cmdArgs[1], cmdArgs[2]);

                    string[] birthdate = cmdArgs[2].Split("/");


                    if (dic.ContainsKey(cmdArgs[2]))
                    {
                        exceptions.Add(cmdArgs[2], birthdate[2]);
                    }
                    else
                    {

                        dic.Add(cmdArgs[2], birthdate[2]);
                    }

                }
            }

            string magicYear = Console.ReadLine();

            List<string> years = ValidYear(dic, magicYear, exceptions);

            foreach (var year in years)
            {
                Console.WriteLine(year);
            }


        }

        private List<string> ValidYear(Dictionary<string, string> dic, string magicYear, Dictionary<string, string> exceptions)
        {
            List<string> years = new List<string>();

            foreach (var item in dic)
            {
                if (item.Value == magicYear)
                {

                    years.Add(item.Key);

                }
            }

            foreach (var item in exceptions)
            {
                if (item.Value == magicYear)
                {
                    years.Add(item.Key);
                }
            }

            return years;
        }
    }
}
