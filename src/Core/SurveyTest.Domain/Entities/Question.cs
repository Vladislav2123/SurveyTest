namespace SurveyTest.Domain.Entities;

public class Question
{
    public Guid Id { get; set; }
    public string Text { get; set; }

    public Survey Survey { get; set; }
    public Guid SurveyId { get; set; }


    public ICollection<Answer> Answers { get; set; }
    public ICollection<Result> Results { get; set; }
}
