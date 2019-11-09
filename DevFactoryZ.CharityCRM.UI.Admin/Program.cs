using System;
using System.Collections.Generic;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    class Program
    {
        static List<ICommand> Commands = new List<ICommand>
        {
        };

        static string Exit = "exit";

        static char ParametersSeparator = ' ';

        static void Main(string[] args)
        {
            var helpCommand = new HelpCommand(Commands);
            Commands.Add(helpCommand);
            string commandName = null;

            Console.WriteLine(
                $"Консоль администратора Charity CRM. Версия {typeof(Program).Assembly.GetName().Version}");

            do
            {
                Console.Write("> ");
                var text = Console.ReadLine(); // read admin command

                var parts = text.Split(ParametersSeparator, StringSplitOptions.RemoveEmptyEntries);
                commandName = parts[0].ToLower(); //parse command

                var commandToExecute = 
                    Commands.FirstOrDefault(command => command.Recognize(commandName));

                if (commandToExecute != null)
                {
                    commandToExecute.Execute(parts.Skip(1).ToArray());
                }
                else if (commandName != Exit)
                {
                    Console.WriteLine($"{commandName} is not recognized. {helpCommand.Help}");
                }
            }
            while (commandName != Exit);
        }
    }
}
