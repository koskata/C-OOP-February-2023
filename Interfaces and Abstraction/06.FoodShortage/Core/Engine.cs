using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _04.BorderControl.Core.Interfaces;
using _04.BorderControl.Models;
using _04.BorderControl.Models.Interfaces;

using _06.FoodShortage.Models;
using _06.FoodShortage.Models.Interfaces;

namespace _04.BorderControl.Core
{
    public class Engine : IEngine
    {
        public void Run()
        {
            int n = int.Parse(Console.ReadLine());

            List<Citizen> citizens = new List<Citizen>();
            List<Rebel> rebels = new List<Rebel>();

            //Dictionary<string, double> citizenWithFood = new Dictionary<string, double>();
            //Dictionary<string, double> rebelWithFood = new Dictionary<string, double>();
            for (int i = 0; i < n; i++)
            {
                string[] cmdArgs = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (cmdArgs.Length == 4)
                {
                    ICitizen citizen = new Citizen(cmdArgs[0], int.Parse(cmdArgs[1]), cmdArgs[2], cmdArgs[3]);
                    citizens.Add((Citizen)citizen);
                    //citizenWithFood.Add(cmdArgs[0], 0);
                }
                else
                {
                    IRebel rebel = new Rebel(cmdArgs[0], int.Parse(cmdArgs[1]), cmdArgs[2]);
                    rebels.Add((Rebel)rebel);
                    //rebelWithFood.Add(cmdArgs[0], 0);
                }
            }


            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                foreach (var citizen in citizens)
                {
                    if (citizen.Name == input)
                    {
                        citizen.BuyFood();
                        break;
                    }
                }

                foreach (var rebel in rebels)
                {
                    if (rebel.Name == input)
                    {
                        rebel.BuyFood();
                        break;
                    }
                }

            }


            double sum = 0;

            foreach (var citizen in citizens)
            {
                sum += citizen.Food;
            }
            foreach (var rebel in rebels)
            {
                sum += rebel.Food;
            }

            Console.WriteLine(sum);
        }


    }
}
