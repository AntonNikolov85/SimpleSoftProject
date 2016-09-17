using System;
using System.Collections.Generic;
using System.Linq;
using SimpleSoftProject.IO;
using SimpleSoftProject.StaticData;

namespace SimpleSoftProject.Models
{
    public class Student
    {
        public string userName;
        public Dictionary<string, Course> enrolledCourses;
        public Dictionary<string, double> marksByCourseName;

        public Student(string userName)
        {
            this.userName = userName;
            this.enrolledCourses = new Dictionary<string, Course>();
            this.marksByCourseName = new Dictionary<string, double>();
        }

        public void EnrollInCourse(Course course)
        {
            if (this.enrolledCourses.ContainsKey(course.name))
            {
                OutputWriter.DisplayException(string.Format(ExceptionMessages.StudentAlreadyEnrolledInGivenCourse,
                    this.userName, course.name));
                return;
            }

            this.enrolledCourses.Add(course.name, course);
        }

        public void SetMarkOnCourse(string courseName, params int[] scores)
        {
            if (!this.enrolledCourses.ContainsKey(courseName))
            {
                OutputWriter.DisplayException(ExceptionMessages.NotEnrolledInCourse);
                return;
            }

            if (scores.Length > Course.NumberOfTaskOnExam)
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidNumberOfScores);
                return;
            }

            this.marksByCourseName.Add(courseName, CalculateMark(scores));
        }

        private double CalculateMark(int[] scores)
        {
            double percentageOfSolvedExam = scores.Sum()/(double) (Course.NumberOfTaskOnExam*Course.MaxScoreOnExamTask);
            double mark = percentageOfSolvedExam * 4 + 2;
            return mark;
        }
    }
}
