using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyTest.Application.Abstraction;
using SurveyTest.Application.Features.Users.Dto;
using SurveyTest.Application.Paging;

namespace SurveyTest.Application.Features.Users.Queries;

public record GetAllUsersQuery(
    string? SearchTerms,
    Page page
) : IRequest<PagedList<UserDto>>;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, PagedList<UserDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllUsersHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PagedList<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var usersQuery = _dbContext.Users.AsQueryable();

        if (string.IsNullOrEmpty(request.SearchTerms) == false)
        {
            usersQuery = usersQuery.Where(user => user.Name.Contains(request.SearchTerms));
        }

        var totalAmount = await usersQuery.CountAsync(cancellationToken);
        var usersList = await usersQuery
            .Skip(request.page.Skip)
            .Take(request.page.size)
            .ToListAsync(cancellationToken);

        var mappedUsers = _mapper.Map<List<UserDto>>(usersList);
        return new PagedList<UserDto>(mappedUsers, totalAmount, request.page);
    }
}
