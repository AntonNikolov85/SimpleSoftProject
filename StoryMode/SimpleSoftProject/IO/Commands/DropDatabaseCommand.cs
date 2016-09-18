using System;
using SimpleSoftProject.Exceptions;
using SimpleSoftProject.Judge;
using SimpleSoftProject.Network;
using SimpleSoftProject.Repository;

namespace SimpleSoftProject.IO.Commands
{
    public class DropDatabaseCommand : Command
    {
        public DropDatabaseCommand(string input, string[] data, Tester tester, StudentsRepository repository, 
            DownloadManager downloadManager, IOManager inputOutputManager) 
            : base(input, data, tester, repository, downloadManager, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 1)
            {
                throw new InvalidCommandException(this.Input);
            }

            this.Repository.UnloadData();
            OutputWriter.WriteMessageOnNewLine("Database dropped!");
        }
    }
}
