using System.Collections.Generic;
using System.Linq;
using DevFactoryZ.CharityCRM.Persistence;
using DevFactoryZ.CharityCRM.UI.Web.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DevFactoryZ.CharityCRM.UI.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class WardCategoriesController : ApiController<IWardCategoryRepository>
    {
        public WardCategoriesController(IWardCategoryRepository repository) : base(repository)
        {
        }

        [HttpGet]
        public ActionResult<IEnumerable<WardCategoryListViewModel>> Get(bool onlyRoot)
        {
            return GetResultWithErrorHandling(
                repository => (onlyRoot ? repository.GetRoots() : repository.GetAll())
                    .Select(model => new WardCategoryListViewModel(model)));
        }

        [HttpGet("{id}")]
        public ActionResult<WardCategoryListViewModel> Get(int id)
        {
            return GetResultWithErrorHandling(
                repository => new WardCategoryListViewModel(repository.GetById(id)));
        }
    }
}
