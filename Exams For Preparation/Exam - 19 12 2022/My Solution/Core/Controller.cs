using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        private SubjectRepository subjects;
        private StudentRepository students;
        private UniversityRepository universities;

        private string[] availableCategories = { "TechnicalSubject", "EconomicalSubject", "HumanitySubject" };

        public Controller()
        {
            subjects = new SubjectRepository();
            students = new StudentRepository();
            universities = new UniversityRepository();
        }

        public string AddStudent(string firstName, string lastName)
        {
            string fullName = firstName + " " + lastName;

            if (students.FindByName(fullName) != null)
            {
                return String.Format(OutputMessages.AlreadyAddedStudent, firstName, lastName);
            }

            IStudent student = new Student(0, firstName, lastName);

            students.AddModel(student);

            return String.Format(OutputMessages.StudentAddedSuccessfully, firstName, lastName, students.GetType().Name);
        }

        public string AddSubject(string subjectName, string subjectType)
        {
            if (!availableCategories.Contains(subjectType))
            {
                return String.Format(OutputMessages.SubjectTypeNotSupported, subjectType);
            }

            if (subjects.FindByName(subjectName) != null)
            {
                return String.Format(OutputMessages.AlreadyAddedSubject, subjectName);
            }

            Subject subject = null;
            if (subjectType == "TechnicalSubject")
            {
                subject = new TechnicalSubject(0, subjectName);
            }

            if (subjectType == "EconomicalSubject")
            {
                subject = new EconomicalSubject(0, subjectName);
            }

            if (subjectType == "HumanitySubject")
            {
                subject = new HumanitySubject(0, subjectName);
            }

            subjects.AddModel(subject);

            return String.Format(OutputMessages.SubjectAddedSuccessfully, subject.GetType().Name, subjectName, subjects.GetType().Name);
        }

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            if (universities.FindByName(universityName) != null)
            {
                return String.Format(OutputMessages.AlreadyAddedUniversity, universityName);
            }

            List<int> ids = requiredSubjects.Select(s => subjects.FindByName(s).Id).ToList();

            IUniversity university = new University(0, universityName, category, capacity, ids);

            universities.AddModel(university);

            return String.Format(OutputMessages.UniversityAddedSuccessfully, universityName, universities.GetType().Name);
        }

        public string ApplyToUniversity(string studentName, string universityName)
        {

            IStudent student = students.FindByName(studentName);
            if (student == null)
            {
                return String.Format(OutputMessages.StudentNotRegitered, student.FirstName, student.LastName);
            }

            IUniversity uni = universities.FindByName(universityName);
            if (uni == null)
            {
                return String.Format(OutputMessages.UniversityNotRegitered, universityName);
            }

            foreach (var reqExam in uni.RequiredSubjects)
            {
                if (!student.CoveredExams.Contains(reqExam))
                {
                    return String.Format(OutputMessages.StudentHasToCoverExams, studentName, universityName);
                }
            }

            if (student.University != null && student.University.Name == uni.Name)
            {
                return String.Format(OutputMessages.StudentAlreadyJoined, student.FirstName, student.LastName, uni.Name);
            }

            student.JoinUniversity(uni);

            return String.Format(OutputMessages.StudentSuccessfullyJoined, student.FirstName, student.LastName, uni.Name);
        }

        public string TakeExam(int studentId, int subjectId)
        {
            IStudent student = students.FindById(studentId);

            if (student == null)
            {
                return OutputMessages.InvalidStudentId;
            }

            ISubject subject = subjects.FindById(subjectId);
            if (subject == null)
            {
                return OutputMessages.InvalidSubjectId;
            }

            if (student.CoveredExams.Contains(subjectId))
            {
                return String.Format(OutputMessages.StudentAlreadyCoveredThatExam, student.FirstName, student.LastName, subject.Name);
            }

            student.CoverExam(subject);

            return String.Format(OutputMessages.StudentSuccessfullyCoveredExam, student.FirstName, student.LastName, subject.Name);
        }

        public string UniversityReport(int universityId)
        {
            IUniversity university = universities.FindById(universityId);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"*** {university.Name} ***");
            sb.AppendLine($"Profile: {university.Category}");


            int studentsCount = 0;
            foreach (var student in students.Models)
            {
                if (student.University?.Id == university.Id)
                {
                    studentsCount++;
                }
            }

            sb.AppendLine($"Students admitted: {studentsCount}");

            int capacityLeft = university.Capacity - studentsCount;
            sb.AppendLine($"University vacancy: {capacityLeft}");

            return sb.ToString().TrimEnd();
        }
    }
}
