using System;
using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    class HelpCommand : ICommand
    {
        readonly IEnumerable<ICommand> commands;

        public HelpCommand(IEnumerable<ICommand> commands)
        {
            this.commands = commands;
        }

        static string Name = "help";

        static string Alias = "?";

        public string Help => $"Type '{Name}' or '{Alias}' for a list of supported commands";

        public void Execute(string[] parameters)
        {
            commands.Each(command => Console.WriteLine($"- {command.Help}"));
        }

        public bool Recognize(string command)
        {
            return command == Name || command == Alias;
        }
    }
}
