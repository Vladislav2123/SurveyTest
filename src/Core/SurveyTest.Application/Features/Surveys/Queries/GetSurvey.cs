using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyTest.Application.Abstraction;
using SurveyTest.Application.Features.Surveys.Dto;
using SurveyTest.Domain.Exceptions;
using SurveyTest.Domain.Entities;

namespace SurveyTest.Application.Features.Surveys.Queries;

public record GetSurveyQuery(Guid Id)
    : IRequest<SurveyDto>;

public class GetSurveyHandler : IRequestHandler<GetSurveyQuery, SurveyDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetSurveyHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<SurveyDto> Handle(GetSurveyQuery request, CancellationToken cancellationToken)
    {
        Survey survey = await _dbContext.Surveys
            .Include(survey => survey.Questions)
            .FirstOrDefaultAsync(survey => survey.Id == request.Id, cancellationToken);

        if(survey == null) throw new EntityNotFoundException(nameof(Survey), request.Id);

        return _mapper.Map<SurveyDto>(survey);
    }
}
