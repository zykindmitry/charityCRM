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
            var commands = InitCommands(services);

            var helpCommand = new HelpCommand(commands);
            commands.Add(helpCommand);           

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

        static List<ICommand> InitCommands(ServiceProvider services)
        {
            return new List<ICommand>
            {
                 new PermissionCreateCommand(services.GetService<ICreateUnitOfWork>()),
                 new PermissionUpdateCommand(services.GetService<ICreateUnitOfWork>()),
                 new PermissionDeleteCommand(services.GetService<ICreateUnitOfWork>()),
                 new PermissionListCommand(
                    services.GetService<ICreateRepository<IPermissionRepository>>()),
                 new PermissionGetCommand(
                    services.GetService<ICreateRepository<IPermissionRepository>>()),
                 new RoleCreateCommand(services.GetService<ICreateUnitOfWork>()),
                 new RoleUpdateCommand(services.GetService<ICreateUnitOfWork>()),
                 new RoleDeleteCommand(services.GetService<ICreateUnitOfWork>()),
                 new RoleListCommand(services.GetService<ICreateRepository<IRoleRepository>>()),
                 new RoleGetCommand(services.GetService<ICreateRepository<IRoleRepository>>()),
                 new RoleAddPermissionCommand(services.GetService<ICreateUnitOfWork>()),
                 new RoleDeletePermissionCommand(services.GetService<ICreateUnitOfWork>()),
                 new FundRegistrationCreateCommand(services.GetService<ICreateUnitOfWork>()),
                 new FundRegistrationDeleteCommand(services.GetService<ICreateUnitOfWork>()),
                 new FundRegistrationListCommand(services.GetService<ICreateRepository<IFundRegistrationRepository>>()),
                 new FundRegistrationUpdateCommand(services.GetService<ICreateUnitOfWork>()),
                 new PasswordConfigCreateCommand(services.GetService<ICreateUnitOfWork>()),
                 new PasswordConfigListCommand(
                    services.GetService<ICreateRepository<IPasswordConfigRepository>>()),
                 new PasswordConfigGetCommand(
                    services.GetService<ICreateRepository<IPasswordConfigRepository>>())
            };
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
