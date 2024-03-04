using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyTest.Application.Features.Queries;
using SurveyTest.Application.Features.Questions.Dto;

namespace SurveyTest.API.Controllers;

[ApiController]
[Route("api/questions")]
public class QuestionController : ControllerBase
{
    private readonly IMediator _mediator;

    public QuestionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{questionId}")]
    public async Task<ActionResult<QuestionDto>> GetQuestionInfo(
        Guid questionId, CancellationToken cancellationToken)
    {
        var query = new GetQuestionQuery(questionId);
        var response = await _mediator.Send(query, cancellationToken);

        return Ok(response);
    }
}
