namespace SurveyTest.Domain.Entities;

public class Answer
{
    public Guid Id { get; set; }
    public string Text { get; set; }

    public Question Question { get; set; }
    public Guid QuestionId { get; set; }

    public ICollection<Result> Results { get; set; }
}
