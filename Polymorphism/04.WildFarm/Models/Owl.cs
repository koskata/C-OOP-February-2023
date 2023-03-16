using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.WildFarm.Models
{
    public class Owl : Bird
    {
        private const double WeightForOwl = 0.25;

        public Owl(string name, double weight, double wingSize)
            : base(name, weight, wingSize)
        {
        }

        public override IReadOnlyCollection<Type> PreferredFoods
            => new HashSet<Type>() { typeof(Meat) };

        public override double WeightMultiplier => WeightForOwl;

        

        public override string ProduceSound()
        {
            return "Hoot Hoot";
        }

        
    }
}
