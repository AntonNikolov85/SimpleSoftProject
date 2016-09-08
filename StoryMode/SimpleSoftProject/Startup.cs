using System;
using SimpleSoftProject.IO;
using SimpleSoftProject.Judge;
using SimpleSoftProject.Repository;

namespace SimpleSoftProject
{
    class Startup
    {
        static void Main(string[] args)
        {
            /*StudentsRepository.InitializeData();
            StudentsRepository.GetAllStudentsFromCourse("Unity");*/
            //Tester.CompareContent(@"E:\Computer Staff\SoftUni\Labs\AdvancedLab\test2.txt", @"E:\Computer Staff\SoftUni\Labs\AdvancedLab\test3.txt");
            //IOManager.TraverseDirectory(0);
            InputReader.StartReadingCommands();
        }
    }
}
