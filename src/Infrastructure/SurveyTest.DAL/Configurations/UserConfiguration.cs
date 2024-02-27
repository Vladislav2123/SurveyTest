using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyTest.Domain;

namespace SurveyTest.DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(user => user.Id);

        builder
            .Property(user => user.Name)
            .IsRequired()
            .HasMaxLength(20);

        builder
            .HasMany(user => user.CreatedSurveys)
            .WithOne(survey => survey.AuthorUser)
            .HasForeignKey(survey => survey.AuthorUserId)
            .IsRequired();
    }
}
