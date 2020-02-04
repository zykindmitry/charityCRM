using DevFactoryZ.CharityCRM.Services;
using DevFactoryZ.CharityCRM.UI.Web.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ApiController<IPermissionService>
    {
        public PermissionsController(IPermissionService service) : base(service)
        { 
        }

        [HttpGet]
        [Authorize]
        public ActionResult<PermissionListModel[]> Get()
        {
            return GetResultWithErrorHandling(
               service => service.GetAll().Select(model => new PermissionListModel(model)).ToArray());
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<PermissionListModel> Get(int id)
        {
            return GetResultWithErrorHandling(
                service => new PermissionListModel(service.GetById(id)));
        }

        [HttpPost]
        [Authorize]
        public ActionResult<PermissionListModel> Post([FromBody]PermissionModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            return GetResultWithErrorHandling(
                service =>
                {
                    var model = service.Create(viewModel.ToDto());
                    return new PermissionListModel(model);
                });
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody]PermissionModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            return ExecuteWithErrorHandling(
                service => service.Update(id, viewModel.ToDto()));                
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            return ExecuteWithErrorHandling(service => service.Delete(id));
        }
    }
}