using AutoMapper;
using SurveyTest.Application.Mapping;
using SurveyTest.Domain.Entities;

namespace SurveyTest.Application.Features.Users.Dto;

public class UserDto : IMapping
{
    public Guid Id { get; init; }
    public string Name { get; init; }

    public void CreateMap(Profile profile)
    {
        profile.CreateMap<User, UserDto>();
    }
}
