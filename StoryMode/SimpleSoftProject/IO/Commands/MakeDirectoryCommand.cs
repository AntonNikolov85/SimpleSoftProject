using SimpleSoftProject.Contracts;
using SimpleSoftProject.Exceptions;


namespace SimpleSoftProject.IO.Commands
{
    public class MakeDirectoryCommand : Command
    {
        public MakeDirectoryCommand(string input, string[] data, IContentComparer tester, IDatabase repository, 
            IDownloadManager downloadManager, IDirectoryManager inputOutputManager) 
            : base(input, data, tester, repository, downloadManager, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }

            string folderName = this.Data[1];
            this.InputOutputManager.CreateDirectoryInCurrentFolder(folderName);
        }
    }
}
