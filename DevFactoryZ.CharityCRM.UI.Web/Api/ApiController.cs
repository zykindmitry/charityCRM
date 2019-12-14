using DevFactoryZ.CharityCRM.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace DevFactoryZ.CharityCRM.UI.Web.Api
{
    public class ApiController<TService> : ControllerBase
    {
        private readonly TService service;

        protected ApiController(TService service)
        {
            this.service = service;
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
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (ValidationException validationError)
            {
                return BadRequest(validationError.Message);
            }
            catch
            {
                // todo: log error
                return StatusCode((int)HttpStatusCode.InternalServerError, "Ошибка на сервере");
            }
        }
    }
}
