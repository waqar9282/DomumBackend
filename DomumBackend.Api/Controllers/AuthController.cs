using DomumBackend.Application.Commands.Auth;
using DomumBackend.Application.Common.Exceptions;
using DomumBackend.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomumBackend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("Login")]
        [ProducesDefaultResponseType(typeof(AuthResponseDTO))]
        public async Task<IActionResult> Login([FromBody] AuthCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { code = ex.FailureCode, message = ex.Message });
            }
        }
    }
}

