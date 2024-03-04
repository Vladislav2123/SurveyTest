using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyTest.Application.Features.Users.Commands;
using SurveyTest.Application.Features.Users.Dto;
using SurveyTest.Application.Features.Users.Queries;
using SurveyTest.Application.Paging;

namespace SurveyTest.API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(
        [FromForm] CreateUserCommand command, 
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(CreateUser), response);
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<UserDto>>> GetAllUsers(
        string? searchTerms,
        int page, int pageSize,
        CancellationToken cancellationToken)
    {
        GetAllUsersQuery query = new(searchTerms, new Page(page, pageSize));
        var response = await _mediator.Send(query, cancellationToken);

        return Ok(response);
    }
}
