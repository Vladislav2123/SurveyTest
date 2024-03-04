using AutoMapper;
using SurveyTest.Application.Mapping;
using SurveyTest.Domain.Entities;

namespace SurveyTest.Application.Features.Surveys.Dto;

public class SurveyLookupDto : IMapping
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public int QuestionsNumber { get; set; }

    public void CreateMap(Profile profile)
    {
        profile.CreateMap<Survey, SurveyLookupDto>()
            .ForMember(dest => dest.QuestionsNumber, 
                opt => opt.MapFrom(src => src.Questions.Count));
    }
}
