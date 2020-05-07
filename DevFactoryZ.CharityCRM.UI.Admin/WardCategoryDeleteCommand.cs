using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для удаления категории подопечного из хранилища.
    /// </summary>
    class WardCategoryDeleteCommand : ICommand
    {
        /// <summary>
        /// Создвет экземпляр <see cref="WardCategoryDeleteCommand"/>.
        /// </summary>
        /// <param name="unitOfWorkCreator">Экземпляр <see cref="ICreateUnitOfWork"/> для работы с хранилищем.</param>
        public WardCategoryDeleteCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private static string CommandText = "delete-ward-category";

        private static string Alias = "dwc";

        private static string IdParameter = "Id категории подопечного";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} (или {Alias}) [{IdParameter}]', чтобы удалить категорию подопечного. "))
            .AppendLine()
            .Append($"    Внимание!!! {IdParameter} можно узнать, выполнив команду 'list-ward-categories' или 'lwc'.")
            .ToString();
             
        private readonly ICreateUnitOfWork unitOfWorkCreator;

        public void Execute(string[] parameters)
        {
            if (!parameters.Any())
            {
                Console.WriteLine($"Ошибка! Отсутствует обязательный параметр '{IdParameter}'");
                return;
            }

            if (!int.TryParse(parameters.First(), out int wardCategoryId))
            {
                Console.WriteLine($"Ошибка! Обязательный параметр '{IdParameter}' должен быть целым положительным числом.");
                return;
            }

            using (var unitOfWork = unitOfWorkCreator.Create())
            {
                var wardCategory = 
                    unitOfWork.GetById<WardCategory, int>(wardCategoryId);

                unitOfWork.Remove(wardCategory);
                unitOfWork.Save();

                Console.WriteLine($"Категория подопечного с идентификатором (ID = {wardCategoryId}) удалена из хранилища.");
            }
        }

        public bool Recognize(string command)
        {
            return command == CommandText || command == Alias;
        }
    }
}
