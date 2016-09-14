using System;
using SimpleSoftProject.IO;
using SimpleSoftProject.Judge;
using SimpleSoftProject.Network;
using SimpleSoftProject.Repository;

namespace SimpleSoftProject
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            Tester tester = new Tester();
            DownloadManager downloadManager = new DownloadManager();
            IOManager ioManager = new IOManager();
            StudentsRepository repo = new StudentsRepository(new RepositoryFilter(), new RepositorySorter());

            CommandInterpreter currentInterpreter = new CommandInterpreter(tester, repo, downloadManager, ioManager);
            InputReader reader = new InputReader(currentInterpreter);

            reader.StartReadingCommands();
        }
    }
}
