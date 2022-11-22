using MediatR;
using Mice.PublisherServices.Features.Events.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mice.PublisherApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult GetEvent([FromQuery]string Id)
        {
            return Ok(_mediator.Send(new GetEventQuery() { Id = Guid.Parse(Id) }));
        }
    }
}
