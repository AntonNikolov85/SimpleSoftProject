﻿namespace SimpleSoftProject.Repository
{
    using IO;
    using Models;
    using System;
    using System.IO;
    using StaticData;
    using System.Linq;
    using Contracts;
    using DataStructures;
    using Exceptions;
    using System.Text.RegularExpressions;
    using System.Collections.Generic;

    public class StudentsRepository : IDatabase
    {
        private bool isDataInitialized;

        private Dictionary<string, Course> courses;
        private Dictionary<string, Student> students;

        private RepositoryFilter filter;
        private RepositorySorter sorter;

        public StudentsRepository(RepositoryFilter filter, RepositorySorter sorter)
        {
            this.filter = filter;
            this.sorter = sorter;
        }

        public bool IsDataInitialized
        {
            get { return this.isDataInitialized; }
        }

        public IReadOnlyDictionary<string, Course> Courses
        {
            get { return this.courses; }
        }

        public IReadOnlyDictionary<string, Student> Students
        {
            get { return this.students; }
        }

        public void LoadData(string fileName)
        {
            if (this.isDataInitialized)
            {
                throw new ArgumentException(ExceptionMessages.DataAlreadyInitializedException);
            }

            this.students = new Dictionary<string, Student>();
            this.courses = new Dictionary<string, Course>();
            ReadData(fileName);
        }

        public void UnloadData()
        {
            if (!this.isDataInitialized)
            {
                throw new ArgumentException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }

            this.students = null;
            this.courses = null;
            this.isDataInitialized = false;
        }

        private void ReadData(string fileName)
        {
            OutputWriter.WriteMessageOnNewLine("Reading data...");
            string path = SessionData.currentPath + "\\" + fileName;

            if (File.Exists(path))
            {
                string pattern = @"([A-Z][a-zA-Z#\++]*_[A-Z][a-z]{2}_\d{4})\s+([A-Za-z]+\d{2}_\d{2,4})\s([\s0-9]+)";
                Regex rgx = new Regex(pattern);
                string[] allInputLines = File.ReadAllLines(path);

                for (int line = 0; line < allInputLines.Length; line++)
                {
                    if (!string.IsNullOrEmpty(allInputLines[line]) && rgx.IsMatch(allInputLines[line]))
                    {
                        Match currentMatch = rgx.Match(allInputLines[line]);
                        string courseName = currentMatch.Groups[1].Value;
                        string username = currentMatch.Groups[2].Value;
                        string scoresStr = currentMatch.Groups[3].Value;

                        try
                        {
                            int[] scores = scoresStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                            if (scores.Any(x => x > 100 || x < 0))
                            {
                                OutputWriter.DisplayException(ExceptionMessages.InvalidScore);
                                continue;
                            }

                            if (scores.Length > UniversityCourse.NumberOfTaskOnExam)
                            {
                                OutputWriter.DisplayException(ExceptionMessages.InvalidNumberOfScores);
                                continue;
                            }

                            if (!students.ContainsKey(username))
                            {
                                this.students.Add(username, new UniversityStudent(username));
                            }

                            if (!this.courses.ContainsKey(courseName))
                            {
                                this.courses.Add(courseName, new UniversityCourse(courseName));
                            }

                            Course course = this.courses[courseName];
                            Student student = this.students[username];

                            student.EnrollInCourse(course);
                            student.SetMarkOnCourse(courseName, scores);

                            course.EnrollStudent(student);
                        }
                        catch (FormatException fex)
                        {
                            throw new FormatException(fex.Message + $"at line : {line}");
                        }
                    }
                }

                isDataInitialized = true;
                OutputWriter.WriteMessageOnNewLine("Data read!");
            }
            else
            {
                throw new InvalidPathException();
            }
        }

        public void GetStudentScoresFromCourse(string courseName, string username)
        {
            if (IsQueryForStudentPossible(courseName, username))
            {
                OutputWriter.PrintStudent(new KeyValuePair<string, double>(username, this.courses[courseName].StudentsByName[username].MarksByCourseName[courseName]));
            }
        }

        public void GetAllStudentsFromCourse(string courseName)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine($"{courseName}:");
                foreach (var studentMarksEntry in this.courses[courseName].StudentsByName)
                {
                    this.GetStudentScoresFromCourse(courseName, studentMarksEntry.Key);
                }
            }
        }

        public void OrderAndTake(string courseName, string comparison, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = this.courses[courseName].StudentsByName.Count;
                }

                Dictionary<string, double> marks = this.courses[courseName].StudentsByName.ToDictionary(x => x.Key, x => x.Value.MarksByCourseName[courseName]);

                this.sorter.OrderAndTake(marks, comparison, studentsToTake.Value);
            }
        }

        public void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = this.courses[courseName].StudentsByName.Count;
                }

                Dictionary<string, double> marks = this.courses[courseName].StudentsByName.ToDictionary(x => x.Key, x => x.Value.MarksByCourseName[courseName]);

                filter.FilterAndTake(marks, givenFilter, studentsToTake.Value);
            }
        }

        private bool IsQueryForCoursePossible(string courseName)
        {
            if (!isDataInitialized)
            {
                throw new ArgumentNullException(nameof(this.isDataInitialized), ExceptionMessages.DataNotInitializedExceptionMessage);
            }

            if (!this.Courses.ContainsKey(courseName))
            {
                throw new NullReferenceException(ExceptionMessages.InexistingCourseInDataBase);
            }

            return true;
        }

        private bool IsQueryForStudentPossible(string courseName, string studentUserName)
        {
            if (!(this.IsQueryForCoursePossible(courseName) && this.Courses[courseName].StudentsByName.ContainsKey(studentUserName)))
            {
                OutputWriter.DisplayException(ExceptionMessages.InexistingStudentInDataBase);
            }

            return true;
        }

        public ISimpleOrderedDataStructure<Course> GetAllCoursesSorted(IComparer<Course> cmp)
        {
            SimpleSortedList<Course> sortedCourses = new SimpleSortedList<Course>();
            sortedCourses.AddAll(courses.Values);

            return sortedCourses;
        }

        public ISimpleOrderedDataStructure<Student> GetAllStudentsSorted(IComparer<Student> cmp)
        {
            SimpleSortedList<Student> sortedStudents = new SimpleSortedList<Student>();
            sortedStudents.AddAll(students.Values);

            return sortedStudents;
        }
    }
}
