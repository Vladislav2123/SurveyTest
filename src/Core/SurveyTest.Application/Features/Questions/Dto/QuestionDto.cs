using AutoMapper;
using SurveyTest.Application.Mapping;
using SurveyTest.Domain.Entities;

namespace SurveyTest.Application.Features.Questions.Dto;

public class QuestionDto : IMapping
{
    public Guid Id { get; init; }
    public string Text { get; init; }

    public AnswerDto[] Answers { get; init; }

    public void CreateMap(Profile profile)
    {
        profile.CreateMap<Question, QuestionDto>();
    }
}
