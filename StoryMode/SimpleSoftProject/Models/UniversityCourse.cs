using System;
using System.Collections.Generic;
using SimpleSoftProject.Contracts;
using SimpleSoftProject.Exceptions;
using SimpleSoftProject.IO;
using SimpleSoftProject.StaticData;

namespace SimpleSoftProject.Models
{
    public class UniversityCourse : Course
    {
        public const int NumberOfTaskOnExam = 10;
        public const int MaxScoreOnExamTask = 100;

        private string name;
        private Dictionary<string, Student> studentsByName;

        public UniversityCourse(string name)
        {
            this.Name = name;
            this.studentsByName = new Dictionary<string, Student>();
        }

        public string Name
        {
            get { return this.name; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }

                this.name = value;
            }
        }

        public IReadOnlyDictionary<string, Student> StudentsByName
        {
            get { return this.studentsByName; }
        }

        public void EnrollStudent(Student student)
        {
            if (this.studentsByName.ContainsKey(student.UserName))
            {
                OutputWriter.DisplayException(string.Format(ExceptionMessages.StudentAlreadyEnrolledInGivenCourse, 
                    student.UserName, this.name));
                return;
            }

            this.studentsByName.Add(student.UserName, student);
        }


    }
}
