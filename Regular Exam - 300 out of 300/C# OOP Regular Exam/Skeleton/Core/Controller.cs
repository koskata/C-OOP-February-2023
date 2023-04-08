using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Utilities.Messages;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private SupplementRepository supplements;
        private RobotRepository robots;

        private string[] validRobotTypes = { nameof(DomesticAssistant), nameof(IndustrialAssistant) };

        private string[] validSuppTypes = { nameof(SpecializedArm), nameof(LaserRadar) };

        public Controller()
        {
            supplements = new SupplementRepository();
            robots = new RobotRepository();
        }

        public string CreateRobot(string model, string typeName)
        {
            if (!validRobotTypes.Contains(typeName))
            {
                return String.Format(OutputMessages.RobotCannotBeCreated, typeName);
            }

            IRobot robot;
            if (typeName == nameof(DomesticAssistant))
            {
                robot = new DomesticAssistant(model);
            }
            else
            {
                robot = new IndustrialAssistant(model);
            }

            robots.AddNew(robot);

            return String.Format(OutputMessages.RobotCreatedSuccessfully, typeName, model);
        }

        public string CreateSupplement(string typeName)
        {
            if (!validSuppTypes.Contains(typeName))
            {
                return String.Format(OutputMessages.SupplementCannotBeCreated, typeName);
            }

            ISupplement supp;
            if (typeName == nameof(LaserRadar))
            {
                supp = new LaserRadar();
            }
            else
            {
                supp = new SpecializedArm();
            }

            supplements.AddNew(supp);
            return String.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);
        }

        public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
        {
            List<IRobot> supportingRobots = new List<IRobot>();

            foreach (var robot in robots.Models())
            {
                if (robot.InterfaceStandards.Contains(intefaceStandard))
                {
                    supportingRobots.Add(robot);
                }
            }

            if (supportingRobots.Count == 0)
            {
                return String.Format(OutputMessages.UnableToPerform, intefaceStandard);
            }

            List<IRobot> newList = supportingRobots.OrderByDescending(x => x.BatteryLevel).ToList();

            int sum = newList.Sum(x => x.BatteryLevel);

            int counter = 0;

            if (sum < totalPowerNeeded)
            {
                return String.Format(OutputMessages.MorePowerNeeded, serviceName, totalPowerNeeded - sum);
            }
            else
            {
                foreach (var robot in newList)
                {
                    if (totalPowerNeeded == 0)
                    {
                        break;
                    }
                    if (robot.BatteryLevel >= totalPowerNeeded)
                    {
                        robot.ExecuteService(totalPowerNeeded);
                        counter++;
                        break;
                    }
                    else
                    {
                        totalPowerNeeded -= robot.BatteryLevel;
                        robot.ExecuteService(robot.BatteryLevel);
                        counter++;
                    }
                }

            }

            return String.Format(OutputMessages.PerformedSuccessfully, serviceName, counter);

        }

        public string Report()
        {
            var ordered = robots.Models().OrderByDescending(x => x.BatteryLevel).ThenBy(x => x.BatteryLevel).ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var item in ordered)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string RobotRecovery(string model, int minutes)
        {
            List<IRobot> list = new List<IRobot>();

            foreach (var robot in robots.Models().Where(x => x.Model == model))
            {
                list.Add(robot);
            }

            int counter = 0;

            foreach (var robot in list.Where(x => x.BatteryLevel < x.BatteryCapacity / 2))
            {
                robot.Eating(minutes);
                counter++;
            }

            return String.Format(OutputMessages.RobotsFed, counter);
        }

        public string UpgradeRobot(string model, string supplementTypeName)
        {
            ISupplement supp = supplements.Models().FirstOrDefault(x => x.GetType().Name == supplementTypeName);

            int interValue = supp.InterfaceStandard;

            List<IRobot> list = new List<IRobot>();

            foreach (var robot in robots.Models())
            {
                if (!robot.InterfaceStandards.Contains(interValue))
                {
                    list.Add(robot);
                }
            }

            List<IRobot> ordered = list.Where(x => x.Model == model).ToList();
            if (ordered.Count == 0)
            {
                return String.Format(OutputMessages.AllModelsUpgraded, model);
            }
            else
            {
                IRobot selectedRobot = ordered.First();

                selectedRobot.InstallSupplement(supp);

                supplements.RemoveByName(supplementTypeName);

                return String.Format(OutputMessages.UpgradeSuccessful, model, supplementTypeName);
            }
        }
    }
}
