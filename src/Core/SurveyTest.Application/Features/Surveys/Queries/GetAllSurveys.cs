using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyTest.Application.Abstraction;
using SurveyTest.Application.Features.Surveys.Dto;
using SurveyTest.Application.Paging;
using SurveyTest.Domain.Entities;

namespace SurveyTest.Application.Features.Surveys.Queries;

public record GetAllSurveysQuery(
    string? SearchTerms,
    Guid? AuthorId,
    string? SortColumn,
    string? SortOrder,
    Page Page
) : IRequest<PagedList<SurveyLookupDto>>;

public class GetAllSurveysHandler: IRequestHandler<GetAllSurveysQuery, PagedList<SurveyLookupDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllSurveysHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PagedList<SurveyLookupDto>> Handle(GetAllSurveysQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Survey> surveysQuery = _dbContext.Surveys
            .Include(survey => survey.Questions)
            .AsQueryable();

        // Filtering
        if (string.IsNullOrEmpty(request.SearchTerms) == false)
        {
            surveysQuery = surveysQuery.Where(survey =>
                survey.Title.Contains(request.SearchTerms));
        }

        if (request.AuthorId != null && request.AuthorId != Guid.Empty)
        {
            surveysQuery = surveysQuery
                .Where(survey => survey.AuthorUserId == request.AuthorId);
        }

        // Sorting
        var sortColumnExpression = GetSortColumnExpression(request);
        if (request.SortOrder?.ToLower() == "asc") surveysQuery = surveysQuery.OrderBy(sortColumnExpression);
        else surveysQuery = surveysQuery.OrderByDescending(sortColumnExpression);

        // Paging
        var totalAmount = surveysQuery.Count();
        var surveys = surveysQuery
            .Skip(request.Page.Skip)
            .Take(request.Page.Take)
            .ToList();

        // Response
        var mappedProducts = _mapper.Map<List<SurveyLookupDto>>(surveys);
        return new PagedList<SurveyLookupDto>(mappedProducts, totalAmount, request.Page);
    }

    private Expression<Func<Survey, object>> GetSortColumnExpression(GetAllSurveysQuery request) =>
        request.SortColumn?.ToLower() switch
        {
            "questions" => survey => survey.Questions.Count,
            _ => survey => survey.CreationDate
        };
}

