using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyTest.Application.Features.Surveys.Commands;
using SurveyTest.Application.Features.Surveys.Dto;
using SurveyTest.Application.Features.Surveys.Queries;
using SurveyTest.Application.Paging;

namespace SurveyTest.API.Controllers;

[ApiController]
[Route("api/surveys")]
public class SurveyController : ControllerBase
{
    private readonly IMediator _mediator;

    public SurveyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<SurveyDto>> CreateSurvey(
        [FromBody] CreateSurveyCommand command,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(CreateSurvey), response);
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<SurveyLookupDto>>> GetAllSurveys(
        string? SearchTerms,
        Guid? AuthorId,
        string? SortColumn,
        string? SortOrder,
        int page, int pageSize,
        CancellationToken cancellationToken)
    {
        var query = new GetAllSurveysQuery(
            SearchTerms, 
            AuthorId, 
            SortColumn, 
            SortOrder, 
            new Page(page, pageSize));
        
        var response = await _mediator.Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpGet("{surveyId}")]
    public async Task<ActionResult<SurveyDto>> GetSurveyById(
        Guid surveyId, CancellationToken cancellationToken)
    {
        var query = new GetSurveyQuery(surveyId);
        var response = await _mediator.Send(query, cancellationToken);

        return response;
    }
}
