using System.Linq;
using DevFactoryZ.CharityCRM.Services;
using DevFactoryZ.CharityCRM.UI.Web.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DevFactoryZ.CharityCRM.UI.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ApiController<IRoleService>
    {
        public RolesController(IRoleService service) : base(service)
        {
        }

        [HttpGet]
        public ActionResult<RoleListViewModel[]> Get()
        {
            return GetResultWithErrorHandling(
               service => service.GetAll().Select(model => new RoleListViewModel(model)).ToArray());
        }

        [HttpGet("{id}")]
        public ActionResult<RoleListViewModel> Get(int id)
        {
            return GetResultWithErrorHandling(
                service => new RoleListViewModel(service.GetById(id)));
        }

        [HttpPost]
        public ActionResult<RoleListViewModel> Post([FromBody]RoleViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            return GetResultWithErrorHandling(
                service =>
                {
                    var model = service.Create(viewModel.ToDto());
                    return new RoleListViewModel(model);
                });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]RoleViewModel viewModel)
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
