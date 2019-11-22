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

        public string Help => $"Напишите '{Name}' or '{Alias}' для получения списка всех поддерживаемых комманд";

        public void Execute(string[] parameters)
        {            
            commands.Each(command => Console.WriteLine($"- {command.Help}"));
            Console.WriteLine("[ ... ] - обязательный параметр");
            Console.WriteLine("( ... ) - необязательный параметр");
        }

        public bool Recognize(string command)
        {
            return command == Name || command == Alias;
        }
    }
}
