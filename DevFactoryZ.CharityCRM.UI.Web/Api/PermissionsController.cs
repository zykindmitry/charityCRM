using DevFactoryZ.CharityCRM.Services;
using DevFactoryZ.CharityCRM.UI.Web.Api.ViewModels;
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
        public ActionResult<PermissionListViewModel[]> Get()
        {
            return GetResultWithErrorHandling(
               service => service.GetAll().Select(model => new PermissionListViewModel(model)).ToArray());
        }

        [HttpGet("{id}")]
        public ActionResult<PermissionListViewModel> Get(int id)
        {
            return GetResultWithErrorHandling(
                service => new PermissionListViewModel(service.GetById(id)));
        }

        [HttpPost]
        public ActionResult<PermissionListViewModel> Post([FromBody]PermissionViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            return GetResultWithErrorHandling(
                service =>
                {
                    var model = service.Create(viewModel.ToDto());
                    return new PermissionListViewModel(model);
                });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]PermissionViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            return ExecuteWithErrorHandling(
                service => service.Update(id, viewModel.ToDto()));                
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return ExecuteWithErrorHandling(service => service.Delete(id));
        }
    }
}