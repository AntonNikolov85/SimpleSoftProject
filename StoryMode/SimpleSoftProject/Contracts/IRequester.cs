using System.Collections.Generic;

namespace SimpleSoftProject.Contracts
{
    public interface IRequester
    {
        void GetStudentScoresFromCourse(string courseName, string username);

        void GetAllStudentsFromCourse(string courseName);

        ISimpleOrderedDataStructure<Course> GetAllCoursesSorted(IComparer<Course> cmp);

        ISimpleOrderedDataStructure<Student> GetAllStudentsSorted(IComparer<Student> cmp);
    }
}
