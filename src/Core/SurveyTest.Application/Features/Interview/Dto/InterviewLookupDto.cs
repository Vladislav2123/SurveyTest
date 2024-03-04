using AutoMapper;
using SurveyTest.Application.Mapping;
using SurveyTest.Domain.Entities;

namespace SurveyTest.Application.Features.Interviews.Dto;

public class InterviewLookupDto : IMapping
{
    public Guid Id { get; init; }

    public string User {get; init; }
    public string Survey { get; init; }

    public void CreateMap(Profile profile)
    {
        profile.CreateMap<Interview, InterviewLookupDto>()
            .ForMember(dest => dest.User,
                opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.Survey, 
                opt => opt.MapFrom(src => src.Survey.Title));
    }
}
