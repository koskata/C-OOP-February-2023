using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class StudentRepository : IRepository<IStudent>
    {
        private List<IStudent> models;

        public StudentRepository()
        {
            models = new List<IStudent>();
        }

        public IReadOnlyCollection<IStudent> Models => models.AsReadOnly();

        public void AddModel(IStudent model)
        {
            Student student = new(models.Count + 1, model.FirstName, model.LastName);
            models.Add(student);
        }

        public IStudent FindById(int id)
            => models.FirstOrDefault(x => x.Id == id);

        public IStudent FindByName(string name)
        {
            string[] fullName = name.Split(' ');

            string firstName = fullName[0];
            string lastName = fullName[1];

            return models.FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName);
        }
    }
}
