using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _03.Telephony.Models.Interfaces;

namespace _03.Telephony.Models
{
    public class Smartphone : ICallable, IBrowsable
    {
        public string Browse(string url)
        {


            if (url.All(c => !Char.IsDigit(c)))
            {
                return $"Browsing: {url}!";
            }

            throw new ArgumentException("Invalid URL!");
        }

        public string Call(string phoneNumber)
        {
            if (phoneNumber.All(c => Char.IsDigit(c)))
            {
                return $"Calling... {phoneNumber}";

            }

            throw new ArgumentException("Invalid number!");
        }
    }
}
