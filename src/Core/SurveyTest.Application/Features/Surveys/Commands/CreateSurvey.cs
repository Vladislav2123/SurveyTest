using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyTest.Application.Abstraction;
using SurveyTest.Application.Features.Surveys.Dto;
using SurveyTest.Domain.Exceptions;
using SurveyTest.Domain.Entities;


namespace SurveyTest.Application.Features.Surveys.Commands;

public record CreateSurveyCommand(
    string Title,
    CreateQuestionDto[] Questions,
    Guid UserId
) : IRequest<SurveyDto>;

public class CreateSurveyValidator : AbstractValidator<CreateSurveyCommand>
{
    public CreateSurveyValidator()
    {
        RuleFor(command => command.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(command => command.Questions)
            .NotNull()
            .NotEmpty()
            .Must(questions => questions.Length <= 100)
            .WithMessage("Questions amount must be less than or equal to 100");

        RuleFor(command => command.UserId)
            .NotEmpty();

        RuleForEach(survey => survey.Questions)
            .SetValidator(new CreateQuestionValidator());
    }
}

public class CreateQuestionValidator : AbstractValidator<CreateQuestionDto>
{
    public CreateQuestionValidator()
    {
        RuleFor(question => question)
            .NotNull();

        RuleFor(question => question.Text)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(question => question.Answers)
            .NotNull()
            .NotEmpty()
            .Must(answers => answers.Length <= 10)
            .WithMessage("Answers amount must be less than or equal to 10");

        RuleForEach(question => question.Answers)
            .ChildRules(answer =>
            {
                answer.RuleFor(answer => answer.Text)
                    .NotEmpty()
                    .MaximumLength(100);
            });
    }
}

public class CreateSurveyHandler : IRequestHandler<CreateSurveyCommand, SurveyDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateSurveyHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<SurveyDto> Handle(CreateSurveyCommand request, CancellationToken cancellationToken)
    {
        if (await _dbContext.Users
            .AnyAsync(user => user.Id == request.UserId, cancellationToken) == false)
            throw new EntityNotFoundException(nameof(User), request.UserId);

        Survey survey = new()
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            AuthorUserId = request.UserId,
            CreationDate = DateTime.UtcNow
        };

        Question[] questions = new Question[request.Questions.Length];

        // Questions Creation
        for (int q = 0; q < request.Questions.Length; q++)
        {
            CreateQuestionDto requestQuestion = request.Questions[q];

            questions[q] = new()
            {
                Id = Guid.NewGuid(),
                Text = requestQuestion.Text,
                SurveyId = survey.Id,
            };

            // Answers creation
            Answer[] answers = new Answer[requestQuestion.Answers.Length];
            var requestAnswers = requestQuestion.Answers.ToList();

            for (int a = 0; a < requestAnswers.Count; a++)
            {
                answers[a] = new()
                {
                    Id = Guid.NewGuid(),
                    Text = requestAnswers[a].Text,
                    QuestionId = questions[q].Id
                };
            }

            questions[q].Answers = answers;
        }

        survey.Questions = questions;

        _dbContext.Surveys.AddAsync(survey, cancellationToken);
        _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<SurveyDto>(survey);
    }
}