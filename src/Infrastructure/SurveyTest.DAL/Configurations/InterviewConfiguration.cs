using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyTest.Domain.Entities;

namespace SurveyTest.DAL.Configurations;

public class InterviewConfiguration : IEntityTypeConfiguration<Interview>
{
    public void Configure(EntityTypeBuilder<Interview> builder)
    {
        builder
            .HasKey(interview => interview.Id);

        builder
            .Property(interview => interview.StartDate)
            .IsRequired();

        builder
            .HasOne(interview => interview.User)
            .WithMany(user => user.Interviews)
            .HasForeignKey(interview => interview.UserId)
            .IsRequired();

        builder
            .HasOne(interview => interview.Survey)
            .WithMany(survey => survey.Interviews)
            .HasForeignKey(interview => interview.SurveyId)
            .IsRequired();

        builder
            .HasMany(interview => interview.Results)
            .WithOne(result => result.Interview)
            .HasForeignKey(result => result.InterviewId)
            .IsRequired();

    }
}
