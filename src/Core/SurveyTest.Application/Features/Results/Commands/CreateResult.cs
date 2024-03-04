using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyTest.Application.Abstraction;
using SurveyTest.Application.Features.Interviews.Queries;
using SurveyTest.Domain;
using SurveyTest.Domain.Entities;
using SurveyTest.Domain.Exceptions;

namespace SurveyTest.Application.Features.Results.Commands;

public record CreateResultCommand(
    Guid UserId,
    Guid SurveyId,
    Guid QuestionId,
    Guid AnswerId
) : IRequest<Unit>;

public class CreateResultValidator : AbstractValidator<CreateResultCommand>
{
    public CreateResultValidator()
    {
        RuleFor(command => command.UserId)
            .NotEmpty();

        RuleFor(command => command.SurveyId)
            .NotEmpty();

        RuleFor(command => command.QuestionId)
            .NotEmpty();

        RuleFor(command => command.AnswerId)
            .NotEmpty();
    }
}

public class CreateResultHandler : IRequestHandler<CreateResultCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMediator _mediator;

    public CreateResultHandler(IApplicationDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(CreateResultCommand request, CancellationToken cancellationToken)
    {
        // Checking that all entities exist
        if (await _dbContext.Users
            .AnyAsync(user => user.Id == request.UserId) == false)
            throw new EntityNotFoundException(nameof(User), request.UserId);

        if (await _dbContext.Surveys
            .AnyAsync(survey => survey.Id == request.SurveyId) == false)
            throw new EntityNotFoundException(nameof(Survey), request.SurveyId);

        if (await _dbContext.Questions
            .AnyAsync(question => question.Id == request.QuestionId) == false)
            throw new EntityNotFoundException(nameof(Question), request.QuestionId);

        if (await _dbContext.Answers
            .AnyAsync(answer => answer.Id == request.AnswerId) == false)
            throw new EntityNotFoundException(nameof(Answer), request.AnswerId);

        // Getting interview
        var getOrCreateInterview = new GetOrCreateInterviewQuery(request.UserId, request.SurveyId);
        Interview interview = await _mediator.Send(getOrCreateInterview, cancellationToken);

        // Checking that same result does not exist
        if (interview.Results
            .Any(result => result.AnswerId == request.AnswerId))
            throw new SameEntityAlreadyExistException(nameof(Result));

        Result result = new()
        {
            Id = Guid.NewGuid(),
            InterviewId = interview.Id,
            QuestionId = request.QuestionId,
            AnswerId = request.AnswerId
        };

        _dbContext.Result.AddAsync(result, cancellationToken);
        _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
