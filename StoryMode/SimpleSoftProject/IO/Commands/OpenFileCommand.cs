using System;
using System.Diagnostics;
using SimpleSoftProject.Exceptions;
using SimpleSoftProject.Judge;
using SimpleSoftProject.Network;
using SimpleSoftProject.Repository;
using SimpleSoftProject.StaticData;

namespace SimpleSoftProject.IO.Commands
{
    public class OpenFileCommand : Command
    {
        public OpenFileCommand(string input, string[] data, Tester tester, StudentsRepository repository, 
            DownloadManager downloadManager, IOManager inputOutputManager) 
            : base(input, data, tester, repository, downloadManager, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }

            string fileName = this.Data[1];
            Process.Start(SessionData.currentPath + "\\" + fileName);
        }
    }
}
