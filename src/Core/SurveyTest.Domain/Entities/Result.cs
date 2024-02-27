namespace SurveyTest.Domain.Entities;

public class Result
{
    public Guid Id { get; set; }
    
    public Interview Interview { get; set; }
    public Guid InterviewId { get; set; }

    public Question Question { get; set; }
    public Guid QuestionId { get; set; }

    public Answer Answer { get; set; }
    public Guid AnswerId { get; set; }

}
