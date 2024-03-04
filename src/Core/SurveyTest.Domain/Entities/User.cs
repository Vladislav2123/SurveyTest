namespace SurveyTest.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public ICollection<Survey> CreatedSurveys { get; set; }
    public ICollection<Interview> Interviews { get; set; }
}
