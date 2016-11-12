using SimpleSoftProject.Contracts;
using SimpleSoftProject.Exceptions;


namespace SimpleSoftProject.IO.Commands
{
    public abstract class Command : IExecutable
    {
        private string input;
        private string[] data;

        private IContentComparer tester;
        private IDatabase repository;
        private IDownloadManager downloadManager;
        private IDirectoryManager inputOutputManager;

        protected Command(string input, string[] data, IContentComparer tester, IDatabase repository, 
            IDownloadManager downloadManager, IDirectoryManager inputOutputManager)
        {
            this.input = input;
            this.data = data;
            this.tester = tester;
            this.repository = repository;
            this.downloadManager = downloadManager;
            this.inputOutputManager = inputOutputManager;
        }

        public string Input
        {
            get { return this.input; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }

                this.input = value;
            }
        }

        public string[] Data
        {
            get { return this.data; }
            private set
            {
                if (value == null || value.Length == 0)
                {
                    throw new InvalidCommandException(this.Input);
                }

                this.data = value;
            }
        }

        protected IDatabase Repository
        {
            get { return this.repository; }
        }

        protected IContentComparer Tester
        {
            get { return this.tester; }
        }

        protected IDirectoryManager InputOutputManager
        {
            get { return this.inputOutputManager; }
        }

        protected IDownloadManager DownloadManager
        {
            get { return this.downloadManager; }
        }

        public abstract void Execute();
    }
}
