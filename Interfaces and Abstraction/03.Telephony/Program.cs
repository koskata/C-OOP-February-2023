using _03.Telephony.Core;
using _03.Telephony.Models;
using _03.Telephony.Models.Interfaces;

namespace _03.Telephony
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