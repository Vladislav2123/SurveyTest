using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyTest.Application.Abstraction;
using SurveyTest.Application.Features.Interviews.Dto;
using SurveyTest.Application.Paging;

namespace SurveyTest.Application.Features.Interviews.Queries;

public record GetAllInterviewsQuery(
    Guid? SurveyId,
    Guid? UserId,
    Page Page
) : IRequest<PagedList<InterviewLookupDto>>;

public class GetAllInterviewsHandler : IRequestHandler<GetAllInterviewsQuery, PagedList<InterviewLookupDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllInterviewsHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PagedList<InterviewLookupDto>> Handle(GetAllInterviewsQuery request, CancellationToken cancellationToken)
    {
        var interviewsQuery = _dbContext.Interviews
            .Include(interview => interview.User)
            .Include(interview => interview.Survey)
            .AsQueryable();

        if(request.SurveyId != null)
            interviewsQuery = interviewsQuery.Where(interview => interview.SurveyId == request.SurveyId);
        else if (request.UserId != null)
            interviewsQuery = interviewsQuery.Where(interview => interview.UserId == request.UserId);

        var totalAmount = await interviewsQuery.CountAsync(cancellationToken);
        var interviews = await interviewsQuery
            .Take(request.Page.Take)
            .Skip(request.Page.Skip)
            .ToListAsync(cancellationToken);

        var mappedInterviews = _mapper.Map<List<InterviewLookupDto>>(interviews);
        return new PagedList<InterviewLookupDto>(mappedInterviews, totalAmount, request.Page);
    }
}

