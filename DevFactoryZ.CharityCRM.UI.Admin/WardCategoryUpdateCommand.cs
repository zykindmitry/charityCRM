using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для изменения наименования категории подопечного в хранилище.
    /// </summary>
    class WardCategoryUpdateCommand : ICommand
    {
        /// <summary>
        /// Создвет экземпляр <see cref="WardCategoryUpdateCommand"/>.
        /// </summary>
        /// <param name="unitOfWorkCreator">Экземпляр <see cref="ICreateUnitOfWork"/> для работы с хранилищем.</param>
        public WardCategoryUpdateCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private static string CommandText = "update-ward-category";

        private static string Alias = "uwc";

        private static string IdParameter = "Id категории подопечного";

        private static string NewNameParameter = "Новое наименование категории подопечного";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} (или {Alias}) [{IdParameter}] [{NewNameParameter}]', чтобы изменить наименование категории подопечного. "))
            .AppendLine()
            .Append($"    Внимание!!! {IdParameter} можно узнать, выполнив команду 'list-ward-categories'.")
            .ToString();
             
        private readonly ICreateUnitOfWork unitOfWorkCreator;

        public void Execute(string[] parameters)
        {
            if (parameters.Length < 2)
            {
                Console.WriteLine($"Ошибка! Отсутствует один или два обязательных параметра: '{IdParameter}', '{NewNameParameter}'");
                return;
            }

            if (!int.TryParse(parameters[0], out int permissionId))
            {
                Console.WriteLine($"Ошибка! Первый обязательный параметр '{IdParameter}' должен быть целым положительным числом.");
                return;
            }

            if (string.IsNullOrWhiteSpace(parameters[1]))
            {
                Console.WriteLine($"Ошибка! Второй обязательный параметр '{NewNameParameter}' должен содержать хотя бы один символ.");
                return;
            }

            using (var unitOfWork = unitOfWorkCreator.Create())
            {
                var permission = 
                    unitOfWork.GetById<WardCategory, int>(permissionId); 

                permission.Name = parameters[1];
                unitOfWork.Save();

                Console.WriteLine($"Наименование категории подопечного с идентификатором (ID = {permission.Id}) изменено.");
            }

        }

        public bool Recognize(string command)
        {
            return command == CommandText || command == Alias;
        }
    }
}
