using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyTest.Application.Abstraction;
using SurveyTest.Domain.Entities;
using SurveyTest.Domain.Exceptions;

namespace SurveyTest.Application.Features.Interviews.Queries;

public record GetInterviewQuery(Guid Id)
    : IRequest<InterviewDto>;

public class GetInterviewHandler : IRequestHandler<GetInterviewQuery, InterviewDto>
{
    public readonly IApplicationDbContext _dbContext;
    public readonly IMapper _mapper;

    public GetInterviewHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<InterviewDto> Handle(GetInterviewQuery request, CancellationToken cancellationToken)
    {
        Interview? interview = await _dbContext.Interviews
            .Include(interview => interview.User)
            .Include(interview => interview.Survey)
            .Include(interview => interview.Results).ThenInclude(result => result.Question)
            .Include(interview => interview.Results).ThenInclude(interview => interview.Answer)
            .FirstOrDefaultAsync(interview => interview.Id == request.Id);

        if (interview == null) throw new EntityNotFoundException(nameof(Interview), request.Id);

        return _mapper.Map<InterviewDto>(interview);
    }
}
