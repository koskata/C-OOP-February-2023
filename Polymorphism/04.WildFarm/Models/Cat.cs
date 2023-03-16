using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.WildFarm.Models
{
    public class Cat : Feline
    {
        private const double WeightForCat = 0.30;

        public Cat(string name, double weight, string livingRegion, string breed)
            : base(name, weight, livingRegion, breed)
        {
        }

        public override IReadOnlyCollection<Type> PreferredFoods
            => new HashSet<Type>() { typeof(Meat), typeof(Vegetable) };

        public override double WeightMultiplier => WeightForCat;

        public override string ProduceSound()
        {
            return "Meow";
        }
    }
}
