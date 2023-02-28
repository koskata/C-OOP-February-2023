namespace _04.PizzaCalories
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Dough White Chewy 100
            try
            {
                string[] pizzaTokens = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string[] doughTokens = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                Dough dough = new(doughTokens[1], doughTokens[2], double.Parse(doughTokens[3]));

                Pizza pizza = new(pizzaTokens[1], dough);
                string input;
                while ((input = Console.ReadLine()) != "END")
                {
                    string[] cmdArgs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    Topping topping = new(cmdArgs[1], double.Parse(cmdArgs[2]));

                    pizza.AddTopping(topping);
                }

                Console.WriteLine(pizza);
            }

            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }
}