using System;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Содержит методы расширений для форматирования вывода информации об исключениях.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Возвращает строку, содержащую сообщение об исключении, включая сообщения всех внутренних исключений.
        /// </summary>
        /// <param name="ex">Основное исключение.</param>
        /// <returns>Сообщение об исключении.</returns>
        public static string GetExceptionMessage(this Exception ex)
        {
            string resultString = String.Empty;
            
            int countInnerEx = 0;
            int countSpaces = 4;

            while (ex != null)
            {
                var exString = new System.Text.StringBuilder(System.Environment.NewLine);
                exString.Append((countInnerEx == 0 ? "<<<<====== Exception " : "<<<<====== Inner Exception ".Insert(0, new string(' ', countInnerEx * countSpaces))));
                exString.Append(countInnerEx == 0 ? string.Empty : (countInnerEx).ToString());
                exString.AppendLine(" begin: ==========>>>");
                exString.Append("Ex type: '".Insert(0, new string(' ', countInnerEx * countSpaces)));
                exString.Append(ex?.GetType().FullName);
                exString.AppendLine("'. ");
                exString.Append("Ex source: ".Insert(0, new string(' ', countInnerEx * countSpaces)));
                exString.Append("Module: '");
                exString.Append(ex?.TargetSite?.Module?.Name ?? string.Empty);
                exString.Append("'. Type: '");
                exString.Append(ex?.TargetSite?.DeclaringType?.FullName ?? string.Empty);
                exString.Append("'. Method: '");
                exString.Append(ex?.TargetSite?.Name ?? string.Empty);
                exString.AppendLine("':");
                exString.Append("Ex message: '".Insert(0, new string(' ', countInnerEx * countSpaces)));
                exString.Append(ex?.Message.Replace(Environment.NewLine, " "));
                exString.AppendLine("'.");
                exString.Append((countInnerEx == 0 ? "<<<<====== Exception " : "<<<<====== Inner Exception ".Insert(0, new string(' ', countInnerEx * countSpaces))));
                exString.Append(countInnerEx == 0 ? string.Empty : countInnerEx.ToString());
                exString.AppendLine(" end. ==========>>>");

                resultString += exString.ToString();
                countInnerEx++;

                ex = ex.InnerException;
            }
            return resultString;
        }

        /// <summary>
        /// Возвращает строку, содержащую полную информацию (в т.ч. стек вызовов) об исключении, включая информацию о всех внутренних исключениях.
        /// </summary>
        /// <param name="ex">Основное исключение.</param>
        /// <returns>Полная информация об исключении.</returns>
        public static string GetFullExceptionInfo(this Exception ex)
        {
            string resultString = String.Empty;
            
            int countInnerEx = 0;
            int countSpaces = 4;

            while (ex != null)
            {

                var str = new System.Text.StringBuilder(System.Environment.NewLine);
                str.Append(countInnerEx == 0 ? "<<<<====== Exception " : "<<<<====== Inner Exception ".Insert(0, new string(' ', countInnerEx * countSpaces)));
                str.Append(countInnerEx == 0 ? string.Empty : (countInnerEx).ToString());
                str.AppendLine(" begin: ==========>>>");
                str.Append("Ex type: '".Insert(0, new string(' ', countInnerEx * countSpaces)));
                str.Append(ex?.GetType().FullName);
                str.AppendLine("'. ");
                str.Append("Ex source: ".Insert(0, new string(' ', countInnerEx * countSpaces)));
                str.Append("Module: '");
                str.Append(ex?.TargetSite?.Module?.Name ?? string.Empty);
                str.Append("'. Type: '");
                str.Append(ex?.TargetSite?.DeclaringType?.FullName ?? string.Empty);
                str.Append("'. Method: '");
                str.Append(ex?.TargetSite?.Name ?? string.Empty);
                str.AppendLine("':");
                str.Append("Ex message: '".Insert(0, new string(' ', countInnerEx * countSpaces)));
                str.Append(ex?.Message.Replace(Environment.NewLine, " "));
                str.AppendLine("'.");
                str.AppendLine("Stack trace begin: ".Insert(0, new string(' ', countInnerEx * countSpaces)));
                str.AppendLine((ex?.StackTrace ?? string.Empty).Insert(0, new string(' ', countInnerEx * countSpaces)));
                str.AppendLine("Stack trace end.".Insert(0, new string(' ', countInnerEx * countSpaces)));
                str.Append(countInnerEx == 0 ? "<<<<====== Exception " : "<<<<====== Inner Exception ".Insert(0, new string(' ', countInnerEx * countSpaces)));
                str.Append(countInnerEx == 0 ? string.Empty : countInnerEx.ToString());
                str.AppendLine(" end. ==========>>>");

                resultString += str.ToString();
                countInnerEx++;

                ex = ex.InnerException;
            }
            return resultString;
        }
    }
}
