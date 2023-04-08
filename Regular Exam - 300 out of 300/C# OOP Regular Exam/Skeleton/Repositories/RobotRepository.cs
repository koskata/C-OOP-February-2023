using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;

namespace RobotService.Repositories
{
    public class RobotRepository : IRepository<IRobot>
    {
        private List<IRobot> models;

        public RobotRepository()
        {
            models = new List<IRobot>();
        }

        public void AddNew(IRobot model)
        {
            models.Add(model);
        }

        public IRobot FindByStandard(int interfaceStandard)
        {
            foreach (var item in models)
            {
                foreach (var ints in item.InterfaceStandards)
                {
                    if (ints == interfaceStandard)
                    {
                        return item;
                    }
                }
            }

            return null;
        }
        //=> models.FirstOrDefault(x => x.InterfaceStandards.Any(x => x == interfaceStandard));

        public IReadOnlyCollection<IRobot> Models()
            => models.AsReadOnly();

        public bool RemoveByName(string typeName)
            => models.Remove(models.FirstOrDefault(x => x.Model == typeName));
    }
}
