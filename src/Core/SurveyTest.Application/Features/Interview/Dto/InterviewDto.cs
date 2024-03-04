using AutoMapper;
using SurveyTest.Application.Mapping;
using SurveyTest.Domain.Entities;

namespace SurveyTest.Application;

public class InterviewDto : IMapping
{
    public Guid Id {get; init; }

    public string User { get; init; }
    public string Survey { get; init; }

    public ResultDto[] Results { get; init; }

    public void CreateMap(Profile profile)
    {
        profile.CreateMap<Interview, InterviewDto>()
            .ForMember(dest => dest.User,
                opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.Survey, 
                opt => opt.MapFrom(src => src.Survey.Title));
    }
}

public class ResultDto : IMapping
{
    public string Question { get; init; }
    public string Answer { get; init; }

    public void CreateMap(Profile profile)
    {
        profile.CreateMap<Result, ResultDto>()
            .ForMember(dest => dest.Question, 
                opt => opt.MapFrom(src => src.Question.Text))
            .ForMember(dest => dest.Answer,
                opt => opt.MapFrom(src => src.Answer.Text));
    }
}
