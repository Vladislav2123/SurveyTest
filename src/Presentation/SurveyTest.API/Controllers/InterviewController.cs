using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyTest.Application;
using SurveyTest.Application.Features.Interviews.Dto;
using SurveyTest.Application.Features.Interviews.Queries;
using SurveyTest.Application.Paging;

namespace SurveyTest.API.Controllers;

[ApiController]
[Route("api/interviews")]
public class InterviewController : ControllerBase
{
    private readonly IMediator _mediator;

    public InterviewController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<InterviewLookupDto>>> GetAllInterviews(
        Guid? SurveyId, Guid? UserId,
        int page, int pageSize,
        CancellationToken cancellationToken)
    {
        var query = new GetAllInterviewsQuery(
            SurveyId, 
            UserId, 
            new Page(page, pageSize));

        var response = await _mediator.Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpGet("{interviewId}")]
    public async Task<ActionResult<InterviewDto>> GetInterviewInfo(
        Guid interviewId, CancellationToken cancellationToken)
    {
        var query = new GetInterviewQuery(interviewId);
        var response = await _mediator.Send(query, cancellationToken);

        return Ok(response);
    }

}
