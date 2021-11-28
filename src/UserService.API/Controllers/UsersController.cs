using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using UserService.Application.Commands;

namespace UserService.API.Controllers
{
    [Route("/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post(CreateUserCommand command)
        {
            bool commandResult = await _mediator.Send(command);

            if (!commandResult)
                return BadRequest();

            return Ok();
        }
    }
}
