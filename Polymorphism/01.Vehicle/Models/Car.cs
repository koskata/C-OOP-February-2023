using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _01.Vehicles.Models.Interfaces;

namespace _01.Vehicles.Models
{
    public class Car : Vehicle
    {
        private const double CarIncreasedFuel = 0.9;

        public Car(double fuelQuantity, double fuelConsumption, double tankCapacity) 
            : base(fuelQuantity, fuelConsumption, tankCapacity, CarIncreasedFuel)
        {
        }
    }
}
