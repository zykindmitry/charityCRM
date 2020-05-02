using System.Linq;
using DevFactoryZ.CharityCRM.Persistence;
using DevFactoryZ.CharityCRM.Services;
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
        public ActionResult<WardCategoryListViewModel[]> Get()
        {
            return GetResultWithErrorHandling(
               repository => repository.GetAll().Select(model => new WardCategoryListViewModel(model)).ToArray());
        }

        [HttpGet("{id}")]
        public ActionResult<WardCategoryListViewModel> Get(int id)
        {
            return GetResultWithErrorHandling(
                repository => new WardCategoryListViewModel(repository.GetById(id)));
        }
    }
}
