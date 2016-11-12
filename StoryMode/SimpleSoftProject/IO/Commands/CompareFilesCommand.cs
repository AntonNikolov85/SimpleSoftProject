using SimpleSoftProject.Contracts;
using SimpleSoftProject.Exceptions;


namespace SimpleSoftProject.IO.Commands
{
    public class CompareFilesCommand : Command
    {
        public CompareFilesCommand(string input, string[] data, IContentComparer tester, IDatabase repository, 
            IDownloadManager downloadManager, IDirectoryManager inputOutputManager) 
            : base(input, data, tester, repository, downloadManager, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 3)
            {
                throw new InvalidCommandException(this.Input);
            }

            string firstPath = this.Data[1];
            string secondPath = this.Data[2];

            this.Tester.CompareContent(firstPath, secondPath);
        }
    }
}
