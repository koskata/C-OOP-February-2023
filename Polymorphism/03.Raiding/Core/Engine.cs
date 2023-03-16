using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _03.Raiding.Core.Interfaces;
using _03.Raiding.Models;
using _03.Raiding.Models.Interfaces;

namespace _03.Raiding.Core
{
    public class Engine : IEngine
    {
        public void Run()
        {
            List<IBaseHero> list = new List<IBaseHero>();

            int n = int.Parse(Console.ReadLine());

            int totalPower = 0;

            for (int i = 0; i < n; i++)
            {
                string heroName = Console.ReadLine();
                string heroType = Console.ReadLine();

                try
                {
                    IBaseHero hero;
                    if (heroType == "Druid")
                    {
                        hero = new Druid(heroName);
                        list.Add(hero);
                        Console.WriteLine(hero.CastAbility());
                    }
                    else if (heroType == "Paladin")
                    {
                        hero = new Paladin(heroName);
                        list.Add(hero);
                        Console.WriteLine(hero.CastAbility());
                    }
                    else if (heroType == "Rogue")
                    {
                        hero = new Rogue(heroName);
                        list.Add(hero);
                        Console.WriteLine(hero.CastAbility());
                    }
                    else if (heroType == "Warrior")
                    {
                        hero = new Warrior(heroName);
                        list.Add(hero);
                        Console.WriteLine(hero.CastAbility());
                    }
                    else
                    {
                        throw new ArgumentException("Invalid hero!");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                

            }

            int bossPower = int.Parse(Console.ReadLine());

            if (totalPower >= bossPower)
            {
                Console.WriteLine("Victory!");
            }
            else
            {
                Console.WriteLine("Defeat...");
            }
        }
    }
}
