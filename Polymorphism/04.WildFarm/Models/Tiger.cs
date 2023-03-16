using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.WildFarm.Models
{
    public class Tiger : Feline
    {
        private const double WeightForTiger = 1.00;

        public Tiger(string name, double weight, string livingRegion, string breed) 
            : base(name, weight, livingRegion, breed)
        {
        }

        public override IReadOnlyCollection<Type> PreferredFoods
            => new HashSet<Type>() { typeof(Meat) };

        public override double WeightMultiplier => WeightForTiger;

        public override string ProduceSound()
        {
            return "ROAR!!!";
        }
    }
}
