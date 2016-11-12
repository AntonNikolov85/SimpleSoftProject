using System;
using System.Collections.Generic;

namespace SimpleSoftProject.Contracts
{
    public interface IDataFilter
    {
        void PrintFilteredStudents(Dictionary<string, double> studentsWithMarks, Predicate<double> givenFilter, int studentsToTake);
    }
}
