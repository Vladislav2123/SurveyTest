using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyTest.Domain.Entities;

namespace SurveyTest.DAL.Configurations;

public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder
            .HasKey(answer => answer.Id);

        builder
            .Property(answer => answer.Text)
            .IsRequired()
            .HasMaxLength(100);
    }
}
