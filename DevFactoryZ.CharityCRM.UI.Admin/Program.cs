using DevFactoryZ.CharityCRM.Persistence.EFCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using DevFactoryZ.CharityCRM.Persistence;

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

            var unitOfWorkCreator = new UnitOfWorkCreator(config.GetConnectionString("dev"));
            var helpCommand = new HelpCommand(Commands);
            Commands.Add(helpCommand);
            Commands.Add(new PermissionCreateCommand(unitOfWorkCreator));
            Commands.Add(new PermissionListCommand(unitOfWorkCreator.CreateRepository<IPermissionRepository>()));
            Commands.Add(new PermissionGetCommand(unitOfWorkCreator.CreateRepository<IPermissionRepository>()));
            Commands.Add(new PermissionUpdateCommand(unitOfWorkCreator));
            Commands.Add(new PermissionDeleteCommand(unitOfWorkCreator));
            string commandName = null;

            Console.WriteLine(
                $"Консоль администратора Charity CRM. Версия {typeof(Program).Assembly.GetName().Version}");

            do
            {
                Console.Write("> ");
                var text = Console.ReadLine(); // read admin command

                var parts = text.Split(ParametersSeparator, StringSplitOptions.RemoveEmptyEntries);
                commandName = parts.Length > 0 ? parts[0].ToLower() : null; //parse command

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
                else if (commandName != Exit && !string.IsNullOrWhiteSpace(commandName))
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
