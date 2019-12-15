using DevFactoryZ.CharityCRM.Ioc;
using DevFactoryZ.CharityCRM.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    class Program
    {
        static string Exit = "exit";

        static char ParametersSeparator = ' ';

        static void Main(string[] args)
        {
            var services = new ServiceCollection()
                .WithJsonConfig("appsettings.json")
                .WithDataAccessComponents("local")
                .BuildServiceProvider();
            var commands = new List<ICommand>();

            var helpCommand = new HelpCommand(commands);
            commands.Add(helpCommand);
            commands.Add(new PermissionCreateCommand(services.GetService<ICreateUnitOfWork>()));
            commands.Add(new PermissionUpdateCommand(services.GetService<ICreateUnitOfWork>()));
            commands.Add(new PermissionDeleteCommand(services.GetService<ICreateUnitOfWork>()));
            commands.Add(new PermissionListCommand(services.GetService<IPermissionRepository>()));
            commands.Add(new PermissionGetCommand(services.GetService<IPermissionRepository>()));

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
                    commands.FirstOrDefault(command => command.Recognize(commandName));

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
