using DevFactoryZ.CharityCRM.Persistence;
using DevFactoryZ.CharityCRM.UI.Web.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionRepository repository;

        public PermissionsController(IPermissionRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public PermissionViewModel[] Get()
        {
            return repository.GetAll().Select(model => new PermissionViewModel(model)).ToArray();
        }
    }
}