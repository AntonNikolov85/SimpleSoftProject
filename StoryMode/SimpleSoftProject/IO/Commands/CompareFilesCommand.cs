using System;
using SimpleSoftProject.Exceptions;
using SimpleSoftProject.Judge;
using SimpleSoftProject.Network;
using SimpleSoftProject.Repository;

namespace SimpleSoftProject.IO.Commands
{
    public class CompareFilesCommand : Command
    {
        public CompareFilesCommand(string input, string[] data, Tester tester, StudentsRepository repository, 
            DownloadManager downloadManager, IOManager inputOutputManager) 
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
