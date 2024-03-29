﻿namespace SurveyTest.Domain.Entities;

public class Survey
{
    public Guid Id { get; set; }
    public string Title { get; set; }

    public User AuthorUser { get; set; }
    public Guid AuthorUserId { get; set; }

    public ICollection<Question> Questions { get; set; }
    public ICollection<Interview> Interviews { get; set; }

    public DateTime CreationDate { get; set; }
}
