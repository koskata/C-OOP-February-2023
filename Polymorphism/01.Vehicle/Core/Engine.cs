using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _01.Vehicles.Core.Interfaces;
using _01.Vehicles.Models;
using _01.Vehicles.Models.Interfaces;

namespace _01.Vehicles.Core
{
    public class Engine : IEngine
    {
        public void Run()
        {
            string[] cmdArgsCar = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string[] cmdArgsTruck = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string[] cmdArgsBus = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            IVehicle car = new Car(double.Parse(cmdArgsCar[1]), double.Parse(cmdArgsCar[2]), double.Parse(cmdArgsCar[3]));
            IVehicle truck = new Truck(double.Parse(cmdArgsTruck[1]), double.Parse(cmdArgsTruck[2]), double.Parse(cmdArgsTruck[3]));
            IVehicle bus = new Bus(double.Parse(cmdArgsBus[1]), double.Parse(cmdArgsBus[2]), double.Parse(cmdArgsBus[3]));

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                

                try
                {
                    string[] cmdArgs = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    if (cmdArgs[0] == "Drive")
                    {
                        if (cmdArgs[1] == "Car")
                        {
                            Console.WriteLine(car.Drive(double.Parse(cmdArgs[2])));
                        }
                        else if (cmdArgs[1] == "Truck")
                        {
                            Console.WriteLine(truck.Drive(double.Parse(cmdArgs[2])));
                        }
                        else
                        {
                            Console.WriteLine(bus.Drive(double.Parse(cmdArgs[2])));
                        }
                    }

                    else if (cmdArgs[0] == "Refuel")
                    {
                        if (cmdArgs[1] == "Car")
                        {
                            car.Refuel(double.Parse(cmdArgs[2]));
                        }
                        else if (cmdArgs[1] == "Truck")
                        {
                            truck.Refuel(double.Parse(cmdArgs[2]));
                        }

                        else
                        {
                            bus.Refuel(double.Parse(cmdArgs[2]));
                        }
                    }

                    else if (cmdArgs[0] == "DriveEmpty")
                    {
                        if (cmdArgs[1] == "Car")
                        {
                            Console.WriteLine(car.Drive(double.Parse(cmdArgs[2])), false);
                        }
                        else if (cmdArgs[1] == "Truck")
                        {
                            Console.WriteLine(truck.Drive(double.Parse(cmdArgs[2])), false);
                        }
                        else
                        {
                            Console.WriteLine(bus.Drive(double.Parse(cmdArgs[2])), false);
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }

            Console.WriteLine(car.ToString());
            Console.WriteLine(truck.ToString());
            Console.WriteLine(bus.ToString());
        }
    }
}
