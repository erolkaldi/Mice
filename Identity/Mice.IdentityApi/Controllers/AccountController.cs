using MediatR;
using Mice.IdentityServices.Features.Companies.Commands;
using Mice.IdentityServices.Features.Customers.Commands;
using Mice.IdentityServices.Features.Users.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mice.IdentityApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public IActionResult GetToken([FromBody] GetTokenCommand cmd)
        {
            return Ok(_mediator.Send(cmd));
        }
        [HttpPost]
        public IActionResult RegisterCompany([FromBody] RegisterCompanyCommand cmd)
        {
            return Ok(_mediator.Send(cmd));
        }
        [HttpPost]
        public IActionResult CustomerLogin([FromBody] GetTokenCommand cmd)
        {
            return Ok(_mediator.Send(cmd));
        }
        [HttpPost]
        public IActionResult RegisterCustomer([FromBody] RegisterCustomerCommand cmd)
        {
            return Ok(_mediator.Send(cmd));
        }
    }
}
