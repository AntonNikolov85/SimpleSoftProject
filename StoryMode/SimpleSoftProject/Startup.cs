using System;
using SimpleSoftProject.IO;
using SimpleSoftProject.Repository;

namespace SimpleSoftProject
{
    class Startup
    {
        static void Main(string[] args)
        {
            StudentsRepository.InitializeData();
            StudentsRepository.GetAllStudentsFromCourse("Unity");
        }
    }
}
