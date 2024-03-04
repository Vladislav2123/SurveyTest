using AutoMapper;
using SurveyTest.Application.Mapping;
using SurveyTest.Domain.Entities;

namespace SurveyTest.Application.Features.Questions.Dto;

public class AnswerDto : IMapping
{
    public Guid Id { get; init; }
    public string Text { get; init; }

    public void CreateMap(Profile profile)
    {
        profile.CreateMap<Answer, AnswerDto>();
    }
}
