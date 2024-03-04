using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyTest.Domain.Entities;

namespace SurveyTest.DAL.Configurations;

public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
{
    public void Configure(EntityTypeBuilder<Survey> builder)
    {
        builder 
            .HasKey(survey => survey.Id);

        builder
            .Property(survey => survey.CreationDate)
            .IsRequired();

        builder
            .Property(survey => survey.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .HasMany(survey => survey.Questions)
            .WithOne(question => question.Survey)
            .HasForeignKey(question => question.SurveyId);
    }
}
