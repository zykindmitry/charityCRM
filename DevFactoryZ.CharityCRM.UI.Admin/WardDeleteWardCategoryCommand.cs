using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для удаления катугории у подопечного БФ.
    /// </summary>
    class WardDeleteWardCategoryCommand : ICommand
    {
        /// <summary>
        /// Создвет экземпляр <see cref="WardDeleteWardCategoryCommand"/>.
        /// </summary>
        /// <param name="unitOfWorkCreator">Экземпляр <see cref="ICreateUnitOfWork"/> для работы с хранилищем.</param>
        public WardDeleteWardCategoryCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private static string CommandText = "update-ward-del-category";

        private static string Alias = "uwdc";

        private static string IdWardParameter = "Id подопечного";

        private static string IdWardCategoryParameter = "Id категории";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} (или {Alias}) [{IdWardParameter}] [{IdWardCategoryParameter}]', чтобы удалить категорию у подопечного. "))
            .AppendLine()
            .AppendLine($"    Внимание!!! {IdWardParameter} можно узнать, выполнив команду 'list-wards' или 'lw'.")
            .Append($"    Внимание!!! {IdWardCategoryParameter} можно узнать, выполнив команду 'list-ward-categories' или 'lwc'.")
            .ToString();

        private readonly ICreateUnitOfWork unitOfWorkCreator;

        public void Execute(string[] parameters)
        {
            if (parameters.Length < 2)
            {
                Console.WriteLine($"Ошибка! Отсутствует один или два обязательных параметра: '{IdWardParameter}', '{IdWardCategoryParameter}'");
                return;
            }

            if (!int.TryParse(parameters[0], out int wardId))
            {
                Console.WriteLine($"Ошибка! Первый обязательный параметр '{IdWardParameter}' должен быть целым положительным числом.");
                return;
            }

            if (!int.TryParse(parameters[1], out int wardCategoryId))
            {
                Console.WriteLine($"Ошибка! Второй обязательный параметр '{IdWardCategoryParameter}' должен быть целым положительным числом.");
                return;
            }

            using (var unitOfWork = unitOfWorkCreator.Create())
            {
                var ward =
                    unitOfWork.GetById<Ward, int>(wardId);

                var wardcCategory =
                    unitOfWork.GetById<WardCategory, int>(wardCategoryId);

                ward.Deny(wardcCategory);
                unitOfWork.Save();

                Console.WriteLine($"Категория '{wardcCategory.Name}' удалена у подопечного '{ward.FullName}'.");
            }

        }

        public bool Recognize(string command)
        {
            return command == CommandText || command == Alias;
        }
    }
}
