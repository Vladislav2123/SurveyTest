using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyTest.Application.Features.Results.Commands;

namespace SurveyTest.API.Controllers;

[ApiController]
[Route("api/results")]
public class ResultController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResultController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> SaveResult(
        [FromForm] CreateResultCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);

        return Ok();
    }
}
