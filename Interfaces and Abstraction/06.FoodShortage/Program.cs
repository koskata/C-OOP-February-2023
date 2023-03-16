using _04.BorderControl.Core;
using _04.BorderControl.Core.Interfaces;

namespace _06.FoodShortage
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