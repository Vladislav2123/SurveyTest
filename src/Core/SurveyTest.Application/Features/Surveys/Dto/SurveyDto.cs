using AutoMapper;
using SurveyTest.Application.Mapping;
using SurveyTest.Domain.Entities;

namespace SurveyTest.Application.Features.Surveys.Dto;

public class SurveyDto : IMapping
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public Guid AuthorId { get; init; }
    public QuestionDto[] Questions { get; init; }
    public DateTime CreationDate { get; init; }

    public void CreateMap(Profile profile)
    {
        profile.CreateMap<Survey, SurveyDto>()
            .ForMember(dest => dest.AuthorId,
                opt => opt.MapFrom(src => src.AuthorUserId));
    }
}

public class QuestionDto : IMapping
{
    public Guid Id { get; set; }
    public string Text { get; set; }

    public void CreateMap(Profile profile)
    {
        profile.CreateMap<Question, QuestionDto>();
    }
}
