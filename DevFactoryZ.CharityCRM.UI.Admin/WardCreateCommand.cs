﻿using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Linq;
using System.Text;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для сохранения нового подопечного БФ в хранилище.
    /// </summary>
    class WardCreateCommand : ICommand
    {
        private const char valueSeparator = ',';

        /// <summary>
        /// Создает экземпляр <see cref="WardCreateCommand"/>
        /// </summary>
        /// <param name="unitOfWorkCreator">Экземпляр <see cref="ICreateUnitOfWork"/> для работы с хранилищем.</param>
        public WardCreateCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private const string CommandText = "create-ward";

        private static string Alias = "cw";

        private readonly string FIOParameter = $"Фамилия,Имя,Отчество - заключить в кавычки, разделять знаком '{valueSeparator}'";

        private const string BirthDateParameter = "Дата рождения";

        private const string PhoneParameter = "Номер телефона";

        private const int FIO = 0;
        
        private const int BirthDate = 1;

        private const int Phone = 2;

        public string Help => ComposeHelpString();

        private readonly ICreateUnitOfWork unitOfWorkCreator;

        public void Execute(string[] parameters)
        {
            if (!parameters.Any())
            {
                Console.WriteLine($"Ошибка! Отсутствует один или два обязательных параметра: '{FIOParameter}', '{BirthDateParameter}'");
                return;
            }

            if (string.IsNullOrWhiteSpace(parameters[FIO]) || !parameters[FIO].Contains(valueSeparator))
            {
                Console.WriteLine($"Ошибка! Первый обязательный параметр '{FIOParameter}' - должен содержать хотя бы один символ.");
                Console.WriteLine($"Ошибка! Разделитель значенй в параметре должен быть '{valueSeparator}'.");
                return;
            }

            if (!DateTime.TryParse(parameters[BirthDate], out DateTime birthDate))
            {
                Console.WriteLine($"Ошибка! Неправильный формат даты во втором обязательном параметре '{BirthDateParameter}'");
                return;
            }

            using (var unitOfWork = unitOfWorkCreator.Create())
            {
                var fioArray = parameters[FIO].Split(valueSeparator);
                int lastName = 0;
                int firstName = 1;
                int midName = 2;

                var wardRegistration =
                    new Ward(
                        new FIO(fioArray[lastName], fioArray[firstName], fioArray[midName])
                        , new Address()
                        , birthDate
                        , parameters[Phone]
                        , Array.Empty<WardCategory>());

                unitOfWork.Add(wardRegistration);
                unitOfWork.Save();

                Console.WriteLine(
                    $"Подопечный '{wardRegistration.FIO.FullName}', {wardRegistration.BirthDate.ToShortDateString()} г.р., создан с идентификатором (ID = {wardRegistration.Id})");
            }
        }

        public bool Recognize(string command)
        {
            return command == CommandText || command == Alias;
        }

        private string ComposeHelpString()
        {
            var sampleWard = new Ward();

            var resultString = new StringBuilder();
            resultString.AppendLine($"Напишите:");
            resultString.Append($" '{CommandText} (или {Alias})");
            resultString.Append($" [{FIOParameter}]");
            resultString.Append($" [{BirthDateParameter}]");
            resultString.Append($" ({PhoneParameter})'");
            resultString.AppendLine(", чтобы создать подопечного.");
            resultString.AppendLine($"Все компоненты ФИО (в т.ч. отсутствующие) обязтельно разделяются знаком '{valueSeparator}'!");

            return resultString.ToString();
        }
    }
}