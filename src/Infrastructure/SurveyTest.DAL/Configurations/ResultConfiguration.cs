using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyTest.Domain.Entities;

namespace SurveyTest.DAL.Configurations;

public class ResultConfiguration : IEntityTypeConfiguration<Result>
{
    public void Configure(EntityTypeBuilder<Result> builder)
    {
        builder
            .HasKey(result => result.Id);

        builder
            .HasOne(result => result.Question)
            .WithMany(question => question.Results)
            .HasForeignKey(result => result.QuestionId);

        builder
            .HasOne(result => result.Answer)
            .WithMany(answer => answer.Results)
            .HasForeignKey(result => result.AnswerId);
    }
}
