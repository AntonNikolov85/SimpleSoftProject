using System;
using System.Collections.Generic;
using System.Linq;
using SimpleSoftProject.Contracts;
using SimpleSoftProject.Exceptions;
using SimpleSoftProject.IO;
using SimpleSoftProject.StaticData;

namespace SimpleSoftProject.Models
{
    public class UniversityStudent : Student
    {
        private string userName;
        private Dictionary<string, Course> enrolledCourses;
        private Dictionary<string, double> marksByCourseName;

        public UniversityStudent(string userName)
        {
            this.UserName = userName;
            this.enrolledCourses = new Dictionary<string, Course>();
            this.marksByCourseName = new Dictionary<string, double>();
        }

        public string UserName
        {
            get { return this.userName; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }

                this.userName = value;
            }
        }

        public IReadOnlyDictionary<string, Course> EnrolledCourses
        {
            get { return this.enrolledCourses; }
        }

        public IReadOnlyDictionary<string, double> MarksByCourseName
        {
            get { return this.marksByCourseName; }
        }

        public void EnrollInCourse(Course course)
        {
            if (this.enrolledCourses.ContainsKey(course.Name))
            {
                throw new DuplicateEntryInStructureException(this.UserName, course.Name);
            }

            this.enrolledCourses.Add(course.Name, course);
        }

        public void SetMarkOnCourse(string courseName, params int[] scores)
        {
            if (!this.enrolledCourses.ContainsKey(courseName))
            {
                throw new KeyNotFoundException(ExceptionMessages.NotEnrolledInCourse);
            }

            if (scores.Length > UniversityCourse.NumberOfTaskOnExam)
            {
                throw new ArgumentException(ExceptionMessages.InvalidNumberOfScores);
            }

            this.marksByCourseName.Add(courseName, CalculateMark(scores));
        }

        public string GetMarkForCourse(string courseName)
        {
            return string.Format($"{this.userName} - {this.MarksByCourseName[courseName]}");
        }

        private double CalculateMark(int[] scores)
        {
            double percentageOfSolvedExam = scores.Sum()/(double) (UniversityCourse.NumberOfTaskOnExam*UniversityCourse.MaxScoreOnExamTask);
            double mark = percentageOfSolvedExam * 4 + 2;
            return mark;
        }
    }
}
