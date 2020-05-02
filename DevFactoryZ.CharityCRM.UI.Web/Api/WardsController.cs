using System.Linq;
using DevFactoryZ.CharityCRM.Services;
using DevFactoryZ.CharityCRM.UI.Web.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DevFactoryZ.CharityCRM.UI.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class WardsController : ApiController<IWardService>
    {
        public WardsController(IWardService service) : base(service)
        {
        }

        [HttpGet]
        public ActionResult<WardListViewModel[]> Get()
        {
            return GetResultWithErrorHandling(
               service => service.GetAll().Select(model => new WardListViewModel(model)).ToArray());
        }

        [HttpGet("{id}")]
        public ActionResult<WardListViewModel> Get(int id)
        {
            return GetResultWithErrorHandling(
                service => new WardListViewModel(service.GetById(id)));
        }

        [HttpPost]
        public ActionResult<WardListViewModel> Post([FromBody]WardViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            return GetResultWithErrorHandling(
                service =>
                {
                    var model = service.Create(viewModel.ToDto());
                    return new WardListViewModel(model);
                });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]WardViewModel viewModel)
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
