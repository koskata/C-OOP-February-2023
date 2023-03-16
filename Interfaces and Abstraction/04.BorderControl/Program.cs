using _04.BorderControl.Core;
using _04.BorderControl.Core.Interfaces;

namespace _04.BorderControl
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IEngine engine = new Engine();
            engine.Run();
        }
    }
}