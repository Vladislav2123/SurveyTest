using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyTest.Domain.Entities;

namespace SurveyTest.DAL.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder
            .HasKey(question => question.Id);

        builder
            .Property(question => question.Text)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .HasMany(question => question.Answers)
            .WithOne(answer => answer.Question)
            .HasForeignKey(answer => answer.QuestionId);
    }
}
