using Microsoft.EntityFrameworkCore;
using SurveyTest.Application.Abstraction;
using SurveyTest.DAL.Configurations;
using SurveyTest.Domain;
using SurveyTest.Domain.Entities;

namespace SurveyTest.DAL;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : 
        base(options) {}

    public DbSet<User> Users { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public DbSet<Survey> Surveys { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public DbSet<Question> Questions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public DbSet<Answer> Answers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public DbSet<Result> Result { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public DbSet<Interview> Interviews { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new SurveyConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionConfiguration());
        modelBuilder.ApplyConfiguration(new AnswerConfiguration());
        modelBuilder.ApplyConfiguration(new InterviewConfiguration());
        modelBuilder.ApplyConfiguration(new ResultConfiguration());
    }
}
