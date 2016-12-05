namespace SimpleSoftProject
{
    using IO;
    using Judge;
    using Network;
    using Repository;
    using Contracts;

    public class Startup
    {
        public static void Main(string[] args)
        {
            IContentComparer tester = new Tester();
            IDownloadManager downloadManager = new DownloadManager();
            IDirectoryManager ioManager = new IOManager();
            IDatabase repo = new StudentsRepository(new RepositoryFilter(), new RepositorySorter());

            IInterpreter currentInterpreter = new CommandInterpreter(tester, repo, downloadManager, ioManager);
            IReader reader = new InputReader(currentInterpreter);

            reader.StartReadingCommands();
        }
    }
}
