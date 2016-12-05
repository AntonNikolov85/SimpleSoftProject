namespace SimpleSoftProject.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class CommandAttribute : Attribute
    {
        private string name;

        public CommandAttribute(string commandName)
        {
            this.name = commandName;
        }

        public string Name => this.name;

        public override bool Equals(object obj)
        {
            return this.name.Equals(obj);
        }
    }
}
