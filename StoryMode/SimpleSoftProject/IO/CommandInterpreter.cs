namespace SimpleSoftProject.IO
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Attributes;
    using Contracts;
    using Commands;

    public class CommandInterpreter : IInterpreter
    {
        private IContentComparer judge;
        private IDatabase repository;
        private IDownloadManager downloadManager;
        private IDirectoryManager inputOutputManager;

        public CommandInterpreter(IContentComparer judge, IDatabase repository, IDownloadManager downloadManager, IDirectoryManager inputOutputManager)
        {
            this.judge = judge;
            this.repository = repository;
            this.downloadManager = downloadManager;
            this.inputOutputManager = inputOutputManager;
        }

        public void InterpredCommand(string input)
        {
            string[] data = input.Split(' ');
            string commandName = data[0].ToLower();

            try
            {
                IExecutable command = this.ParseCommand(input, commandName, data);
                command.Execute();
            }
            catch (DirectoryNotFoundException dnfe)
            {
                OutputWriter.DisplayException(dnfe.Message);
            }
            catch (ArgumentOutOfRangeException aore)
            {
                OutputWriter.DisplayException(aore.Message);
            }
            catch (ArgumentException ae)
            {
                OutputWriter.DisplayException(ae.Message);
            }
            catch (Exception e)
            {
                OutputWriter.DisplayException(e.Message);
            }
        }

        private IExecutable ParseCommand(string input, string command, string[] data)
        {
            object[] parametersForConstruction = new object[] {input, data};

            var typeOfCommand = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .First(t => t.GetCustomAttributes(typeof(CommandAttribute))
                    .Where(a => a.Equals(command))
                    .ToArray().Length > 0);

            var typeOfInterpreter = typeof(CommandInterpreter);

            Command cmd = (Command)Activator.CreateInstance(typeOfCommand, parametersForConstruction);

            FieldInfo[] fieldsOfCommand = typeOfCommand.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo[] fieldsofInterpreter = typeOfInterpreter.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var fieldOfCommand in fieldsOfCommand)
            {
                var atr = fieldOfCommand.GetCustomAttribute(typeof(InjectAttribute));
                if (atr != null)
                {
                    if (fieldsofInterpreter.Any(x => x.FieldType == fieldOfCommand.FieldType))
                    {
                        fieldOfCommand.SetValue(cmd, fieldsofInterpreter.First(x => x.FieldType == fieldOfCommand.FieldType).GetValue(this));
                    }
                }
            }

            return cmd;
        }
    }
}
