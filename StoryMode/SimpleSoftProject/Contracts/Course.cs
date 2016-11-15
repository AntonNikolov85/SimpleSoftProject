using System;
using System.Collections.Generic;

namespace SimpleSoftProject.Contracts
{
    public interface Course : IComparable<Course>
    {
        string Name { get; }

        IReadOnlyDictionary<string, Student> StudentsByName { get; }

        void EnrollStudent(Student student);
    }
}
