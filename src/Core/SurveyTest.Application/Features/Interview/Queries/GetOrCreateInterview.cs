using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyTest.Application.Abstraction;
using SurveyTest.Domain.Entities;

namespace SurveyTest.Application.Features.Interviews.Queries;

public record GetOrCreateInterviewQuery(
    Guid UserId, Guid SurveyId
) : IRequest<Interview>;

public class GetOrCreateInterviewHandler : IRequestHandler<GetOrCreateInterviewQuery, Interview>
{
    private readonly IApplicationDbContext _dbContext;

    public GetOrCreateInterviewHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Interview> Handle(GetOrCreateInterviewQuery request, CancellationToken cancellationToken)
    {
        Interview? interview = await _dbContext.Interviews
            .Include(interview => interview.Results)
            .FirstOrDefaultAsync(
                interview => interview.UserId == request.UserId &&
                interview.SurveyId == request.SurveyId,
                cancellationToken);

        if (interview == null)
        {
            interview = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                SurveyId = request.SurveyId,
                StartDate = DateTime.UtcNow,
                Results = new List<Result>()
            };

            _dbContext.Interviews.AddAsync(interview, cancellationToken);
            _dbContext.SaveChangesAsync(cancellationToken);
        }

        return interview;
    }
}
