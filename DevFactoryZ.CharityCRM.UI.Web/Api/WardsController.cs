using System;
using System.Collections.Generic;
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
        private readonly IWardCategoryService categoryService;

        public WardsController(IWardService service, IWardCategoryService categoryService) 
            : base(service)
        {
            this.categoryService = categoryService ??
                throw new ArgumentNullException(nameof(categoryService));
        }

        [HttpGet()]
        public ActionResult<WardListViewModel[]> Get(int? categoryId)
        {
            return GetResultWithErrorHandling(
               service => service
                .GetAll()
                    .Where(ward =>
                        categoryId == null 
                            ? true 
                            : ward.WardCategories.Any(c => c.WardCategory.Id == categoryId ))
                    .Select(ward => new WardListViewModel(ward))
                        .ToArray());
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
                    viewModel.WardCategories = GetTrackedEFEntities(viewModel.WardCategories);

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
                service => 
                {
                    viewModel.WardCategories = GetTrackedEFEntities(viewModel.WardCategories);

                    service.Update(id, viewModel.ToDto());
                });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return ExecuteWithErrorHandling(service => service.Delete(id));
        }

        private IEnumerable<WardCategory> GetTrackedEFEntities(IEnumerable<WardCategory> entities)
        {
            var result = new HashSet<WardCategory>();

            entities?.Each(e => result.Add(categoryService.GetById(e.Id)));

            return result;
        }
    }
}
