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

        public string Help => $"Введите '{Name}' или '{Alias}' для получения списка всех поддерживаемых комманд";

        public void Execute(string[] parameters)
        {
            Console.WriteLine("Синтаксис:");
            Console.WriteLine("<Команда> <обязательные параметры> <обязательные опции> <дополнительные опции>");
            Console.WriteLine("Поддерживаемые команды:");
            Console.WriteLine("");
            commands.Each(command => Console.WriteLine($"- {command.Help}"));
        }

        public bool Recognize(string command)
        {
            return command == Name || command == Alias;
        }
    }
}
