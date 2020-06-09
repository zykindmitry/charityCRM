using DevFactoryZ.CharityCRM.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace DevFactoryZ.CharityCRM.UI.Web.Api
{
    public class ApiController<TService> : ControllerBase
    {
        private readonly TService service;

        private readonly ILoggerFactory loggerFactory = new LoggerFactory();

        private readonly ILogger logger;

        protected ApiController(TService service)
        {
            this.service = service;

            loggerFactory.AddProvider(new DebugLoggerProvider());

            logger = loggerFactory.CreateLogger<TService>();
        }

        /// <summary>
        /// Выполняет вызов сервиса и обрабатывает стандартные ошибки
        /// </summary>
        /// <typeparam name="T">Тип результата действия контроллера</typeparam>
        /// <param name="serviceCall">Вызов сервиса</param>
        /// <returns>Рельтат действия контроллера</returns>
        protected ActionResult<T> GetResultWithErrorHandling<T>(Func<TService, T> serviceCall)
        {
            T result = default;
            ExecuteWithErrorHandling(s => result = serviceCall(s));

            return result;
        }

        /// <summary>
        /// Выполняет вызов сервиса и обрабатывает стандартные ошибки
        /// </summary>
        /// <typeparam name="T">Тип результата действия контроллера</typeparam>
        /// <param name="serviceCall">Вызов сервиса</param>
        /// <returns>Рельтат действия контроллера</returns>
        protected ActionResult ExecuteWithErrorHandling(Action<TService> serviceCall)
        {
            try
            {
                serviceCall(service);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Ошибка репозитория");

                return NotFound();
            }
            catch (ValidationException validationError)
            {
                logger.LogError(validationError, "Ошибка валидации");

                return BadRequest(validationError.Message);
            }
            catch (Exception ex)
            {
                // todo: log error
                logger.LogError(ex, "Ошибка на сервере");

                return StatusCode((int)HttpStatusCode.InternalServerError, "Ошибка на сервере");
            }
        }
    }
}
