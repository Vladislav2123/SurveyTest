using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SurveyTest.Domain;
using SurveyTest.Domain.Entities;

namespace SurveyTest.Application.Abstraction;

public interface IApplicationDbContext
{
    public DatabaseFacade Database { get; }

    public DbSet<User> Users { get; set; }
    public DbSet<Survey> Surveys { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Result> Result { get; set; }
    public DbSet<Interview> Interviews { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();
}
