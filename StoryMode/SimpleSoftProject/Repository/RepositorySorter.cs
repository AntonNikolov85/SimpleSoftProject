namespace SimpleSoftProject.Repository
{
    using IO;
    using StaticData;
    using System.Linq;
    using Contracts;
    using System.Collections.Generic;

    public class RepositorySorter : IDataSorter
    {
        public void OrderAndTake(Dictionary<string, double> studentsWithMarks, string comparison, int studentsToTake)
        {
            comparison = comparison.ToLower();
            if (comparison == "ascending")
            {
                this.PrintSortedStudents(studentsWithMarks.OrderBy(x => x.Value)
                                        .Take(studentsToTake)
                                        .ToDictionary(pair => pair.Key, pair => pair.Value));
            }
            else if (comparison == "descending")
            {
                this.PrintSortedStudents(studentsWithMarks.OrderByDescending(x => x.Value)
                                        .Take(studentsToTake)
                                        .ToDictionary(pair => pair.Key, pair => pair.Value));
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidComparisonQuery);
            }
        }

        public void PrintSortedStudents(Dictionary<string, double> studentsSorted)
        {
            foreach (KeyValuePair<string, double> keyValuePair in studentsSorted)
            {
                OutputWriter.WriteMessageOnNewLine(string.Format($"{keyValuePair.Key} - {keyValuePair.Value}"));
            }
        }
    }
}
