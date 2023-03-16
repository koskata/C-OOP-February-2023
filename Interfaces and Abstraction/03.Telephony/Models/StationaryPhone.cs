using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _03.Telephony.Models.Interfaces;

namespace _03.Telephony.Models
{
    public class StationaryPhone : ICallable
    {
        public string Call(string phoneNumber)
        {
            if (phoneNumber.All(c => Char.IsDigit(c)))
            {
                return $"Dialing... {phoneNumber}";

            }

            throw new ArgumentException("Invalid number!");
        }
    }
}
