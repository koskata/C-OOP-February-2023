using _01.Vehicles.Core;
using _01.Vehicles.Core.Interfaces;

namespace _01.Vehicles
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