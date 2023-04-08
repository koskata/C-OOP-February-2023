using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PlanetWars.Core.Contracts;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Utilities.Messages;

namespace PlanetWars.Core
{
    public class Controller : IController
    {
        private PlanetRepository planets;
        private string[] availableMilTypes = { nameof(SpaceForces), nameof(StormTroopers), nameof(AnonymousImpactUnit) };
        
        private string[] availableWeaponTypes = { nameof(BioChemicalWeapon), nameof(NuclearWeapon), nameof(SpaceMissiles) };

        public Controller()
        {
            planets = new PlanetRepository();
        }

        public string AddUnit(string unitTypeName, string planetName)
        {
            IPlanet planet = planets.FindByName(planetName);

            if (planet == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (!availableMilTypes.Contains(unitTypeName))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.ItemNotAvailable, unitTypeName));
            }

            if (planet.Army.Any(x => x.GetType().Name == unitTypeName))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planetName));
            }

            IMilitaryUnit unit;
            if (unitTypeName == nameof(AnonymousImpactUnit))
            {
                unit = new AnonymousImpactUnit();
            }
            else if (unitTypeName == nameof(SpaceForces))
            {
                unit = new SpaceForces();
            }
            else
            {
                unit = new StormTroopers();
            }


            planet.AddUnit(unit);
            planet.Spend(unit.Cost);

            return String.Format(OutputMessages.UnitAdded, unitTypeName, planetName);
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            IPlanet planet = planets.FindByName(planetName);

            if (planet == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (planet.Weapons.Any(x => x.GetType().Name == weaponTypeName))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName, planetName));
            }

            if (!availableWeaponTypes.Contains(weaponTypeName))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName));
            }

            IWeapon weapon;
            if (weaponTypeName == nameof(BioChemicalWeapon))
            {
                weapon = new BioChemicalWeapon(destructionLevel);
            }
            else if (weaponTypeName == nameof(NuclearWeapon))
            {
                weapon = new NuclearWeapon(destructionLevel);
            }
            else
            {
                weapon = new SpaceMissiles(destructionLevel);
            }

            planet.AddWeapon(weapon);
            planet.Spend(weapon.Price);

            return String.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);
        }

        public string CreatePlanet(string name, double budget)
        {
            IPlanet planet = new Planet(name, budget);

            if (planets.FindByName(name) != null)
            {
                return String.Format(OutputMessages.ExistingPlanet, name);
            }


            planets.AddItem(planet);
            return String.Format(OutputMessages.NewPlanet, name);
        }

        public string ForcesReport()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");

            var ordered = planets.Models.OrderByDescending(x => x.MilitaryPower)
                .ThenBy(x => x.Name);

            foreach (var item in ordered)
            {
                sb.AppendLine(item.PlanetInfo());
            }

            return sb.ToString().TrimEnd();
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            IPlanet planet = planets.FindByName(planetOne);
            IPlanet planet2 = planets.FindByName(planetTwo);

            if (planet.MilitaryPower == planet2.MilitaryPower)
            {
                bool first = planet.Weapons.Any(x => x.GetType().Name == nameof(NuclearWeapon));
                bool second = planet2.Weapons.Any(x => x.GetType().Name == nameof(NuclearWeapon));

                if ((first && second) || (first == false && second == false))
                {
                    planet.Spend(planet.Budget / 2);
                    planet2.Spend(planet2.Budget / 2);

                    return OutputMessages.NoWinner;
                }

                if (first == true && second == false)
                {
                    planet.Spend(planet.Budget / 2);
                    planet.Profit(planet2.Budget / 2);

                    planet.Profit(planet2.Army.Sum(s => s.Cost));
                    planet.Profit(planet2.Weapons.Sum(s => s.Price));

                    planets.RemoveItem(planetTwo);

                    return String.Format(OutputMessages.WinnigTheWar, planet.Name, planet2.Name);
                }
                else
                {
                    planet2.Spend(planet2.Budget / 2);
                    planet2.Profit(planet.Budget / 2);

                    planet2.Profit(planet.Army.Sum(s => s.Cost));
                    planet2.Profit(planet.Weapons.Sum(s => s.Price));

                    planets.RemoveItem(planetOne);

                    return String.Format(OutputMessages.WinnigTheWar, planet2.Name, planet.Name);
                }
            }

            else
            {
                if (planet.MilitaryPower > planet2.MilitaryPower)
                {
                    planet.Spend(planet.Budget / 2);
                    planet.Profit(planet2.Budget / 2);

                    planet.Profit(planet2.Army.Sum(s => s.Cost));
                    planet.Profit(planet2.Weapons.Sum(s => s.Price));

                    planets.RemoveItem(planetTwo);

                    return String.Format(OutputMessages.WinnigTheWar, planet.Name, planet2.Name);
                }
                else
                {
                    planet2.Spend(planet2.Budget / 2);
                    planet2.Profit(planet.Budget / 2);

                    planet2.Profit(planet.Army.Sum(s => s.Cost));
                    planet2.Profit(planet.Weapons.Sum(s => s.Price));

                    planets.RemoveItem(planetOne);

                    return String.Format(OutputMessages.WinnigTheWar, planet2.Name, planet.Name);
                }
            }
        }

        public string SpecializeForces(string planetName)
        {
            IPlanet planet = planets.FindByName(planetName);

            if (planet == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (planet.Army.Count == 0)
            {
                return ExceptionMessages.NoUnitsFound;
            }

            planet.Spend(1.25);
            planet.TrainArmy();

            return String.Format(OutputMessages.ForcesUpgraded, planetName);
        }

    }
}
