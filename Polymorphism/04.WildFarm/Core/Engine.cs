using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _04.WildFarm.Core.Interfaces;
using _04.WildFarm.Models;
using _04.WildFarm.Models.Interfaces;

namespace _04.WildFarm.Core
{
    public class Engine : IEngine
    {
        private readonly ICollection<IAnimal> animals = new List<IAnimal>();

        public void Run()
        {
            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] animalArgs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string[] foodArgs = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);


                IAnimal animal = null;

                try
                {

                    switch (animalArgs[0])
                    {
                        case "Owl":
                            animal = new Owl(animalArgs[1], double.Parse(animalArgs[2]), double.Parse(animalArgs[3]));
                            break;
                        case "Hen":
                            animal = new Hen(animalArgs[1], double.Parse(animalArgs[2]), double.Parse(animalArgs[3]));
                            break;
                        case "Mouse":
                            animal = new Mouse(animalArgs[1], double.Parse(animalArgs[2]), animalArgs[3]);
                            break;
                        case "Dog":
                            animal = new Dog(animalArgs[1], double.Parse(animalArgs[2]), animalArgs[3]);
                            break;
                        case "Cat":
                            animal = new Cat(animalArgs[1], double.Parse(animalArgs[2]), animalArgs[3], animalArgs[4]);
                            break;
                        case "Tiger":
                            animal = new Tiger(animalArgs[1], double.Parse(animalArgs[2]), animalArgs[3], animalArgs[4]);
                            break;
                        default:
                            throw new ArgumentException("Invalid animal type!");

                    }

                    IFood food;

                    switch (foodArgs[0])
                    {
                        case "Vegetable":
                            food = new Vegetable(int.Parse(foodArgs[1]));
                            break;
                        case "Fruit":
                            food = new Fruit(int.Parse(foodArgs[1]));
                            break;
                        case "Meat":
                            food = new Meat(int.Parse(foodArgs[1]));
                            break;
                        case "Seeds":
                            food = new Seeds(int.Parse(foodArgs[1]));
                            break;
                        default:
                            throw new ArgumentException("Invalid food type!");
                    }


                    Console.WriteLine(animal.ProduceSound());
                    animal.Eat(food);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception)
                {
                    throw;
                }

                animals.Add(animal);

            }

            foreach (IAnimal animal in animals)
            {
                Console.WriteLine(animal);
            }
        }
    }
}
