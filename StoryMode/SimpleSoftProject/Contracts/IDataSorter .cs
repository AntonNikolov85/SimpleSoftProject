using System.Collections.Generic;

namespace SimpleSoftProject.Contracts
{
    public interface IDataSorter
    {
        void PrintSortedStudents(Dictionary<string, double> studentsSorted);
    }
}
