using AutoMapper;
using MediatR;
using SurveyTest.Application.Abstraction;
using SurveyTest.Application.Features.Questions.Dto;
using SurveyTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SurveyTest.Domain.Exceptions;

namespace SurveyTest.Application.Features.Questions.Queries;

public record GetQuestionQuery(Guid Id)
    : IRequest<QuestionDto>;

public class GetQuestionHandler : IRequestHandler<GetQuestionQuery, QuestionDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetQuestionHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<QuestionDto> Handle(GetQuestionQuery request, CancellationToken cancellationToken)
    {
        Question question = await _dbContext.Questions
            .Select(q => new Question() { Id = q.Id, Text = q.Text, Answers = q.Answers })
            .FirstOrDefaultAsync(question => question.Id == request.Id, cancellationToken);

        if(question == null) throw new EntityNotFoundException(nameof(Question), request.Id);

        return _mapper.Map<QuestionDto>(question);
    }
}
