namespace SimpleSoftProject.IO.Commands
{
    using Attributes;
    using Contracts;
    using Exceptions;

    [Command("cmp")]
    public class CompareFilesCommand : Command
    {
        [Inject]
        private IContentComparer tester;

        public CompareFilesCommand(string input, string[] data) 
            : base(input, data)
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

            this.tester.CompareContent(firstPath, secondPath);
        }
    }
}
