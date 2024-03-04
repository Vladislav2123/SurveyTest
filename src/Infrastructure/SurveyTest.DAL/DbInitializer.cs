using SurveyTest.Application.Abstraction;

namespace SurveyTest.DAL;

public class DbInitializer
{
    public static void Initialize(IApplicationDbContext dbContext)
    {
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
}
