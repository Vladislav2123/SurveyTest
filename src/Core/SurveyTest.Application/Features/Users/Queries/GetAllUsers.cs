using AutoMapper;
using MediatR;
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

    public Task<PagedList<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var usersQuery = _dbContext.Users.AsQueryable();

        if (string.IsNullOrEmpty(request.SearchTerms) == false)
        {
            usersQuery = usersQuery.Where(user => user.Name.Contains(request.SearchTerms));
        }

        var totalAmount = usersQuery.Count();
        var usersList = usersQuery
            .Skip(request.page.Skip)
            .Take(request.page.size)
            .ToList();

        var mappedUsers = _mapper.Map<List<UserDto>>(usersList);
        return Task.FromResult(new PagedList<UserDto>(mappedUsers, totalAmount, request.page));
    }
}
