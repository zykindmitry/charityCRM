using DevFactoryZ.CharityCRM.Services;
using DevFactoryZ.CharityCRM.UI.Web.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DevFactoryZ.CharityCRM.UI.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ApiController<IAccountService>
    {
        public AccountsController(IAccountService servce) : base(servce)
        {
                
        }

        // GET: api/<controller>
        [HttpGet]
        [Authorize]
        public ActionResult<AccountListModel[]> Get()
        {
            return GetResultWithErrorHandling(
                service => service.GetAll().Select(model => new AccountListModel(model)).ToArray());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<AccountListModel> Get(int id)
        {
            return GetResultWithErrorHandling(
                service => new AccountListModel(service.GetById(id)));
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<AccountListModel> Post([FromBody]AccountModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            return GetResultWithErrorHandling(
                service =>
                {
                    var model = service.Create(viewModel.ToDomain());
                    return new AccountListModel(model);
                });
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody]AccountModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            return ExecuteWithErrorHandling(
                service => service.Update(viewModel.Login, viewModel.PasswordClearText, viewModel.PasswordConfig));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            return ExecuteWithErrorHandling(service => service.Delete(id));
        }
    }
}
