using DevFactoryZ.CharityCRM.Persistence.EFCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    class Program
    {
        static List<ICommand> Commands = new List<ICommand>();

        static string Exit = "exit";

        static char ParametersSeparator = ' ';

        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var unitOfWorkCreator = new UnitOfWorkCreator(config, "dev");
            var helpCommand = new HelpCommand(Commands);
            Commands.Add(helpCommand);
            Commands.Add(new PermissionCreateCommand(unitOfWorkCreator));
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
                    try
                    {
                        commandToExecute.Execute(parts.Skip(1).ToArray());
                    }
                    catch(Exception error)
                    {
                        WriteException(error);
                    }
                }
                else if (commandName != Exit)
                {
                    Console.WriteLine($"{commandName} is not recognized. {helpCommand.Help}");
                }
            }
            while (commandName != Exit);
        }

        static void WriteException(Exception error)
        {
            Console.WriteLine(error.Message);
            Console.WriteLine(error.StackTrace);

            if (error.InnerException != null)
            {
                Console.WriteLine();
                Console.WriteLine("Внутреннее исключение:");
                WriteException(error.InnerException);
            }
        }
    }
}
