using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.WildFarm.Models
{
    public class Hen : Bird
    {

        private const double WeightForHen = 0.35;
        public Hen(string name, double weight, double wingSize)
            : base(name, weight, wingSize)
        {
        }

        public override IReadOnlyCollection<Type> PreferredFoods 
            => new HashSet<Type>() { typeof(Meat), typeof(Vegetable), typeof(Seeds), typeof(Fruit)};

        public override double WeightMultiplier => WeightForHen;

        public override string ProduceSound()
        {
            return "Cluck";
        }
    }
}
