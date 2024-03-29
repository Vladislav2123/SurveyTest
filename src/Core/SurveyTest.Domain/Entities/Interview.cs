﻿namespace SurveyTest.Domain.Entities;

public class Interview
{
    public Guid Id { get; set; }

    public User User { get; set; }
    public Guid UserId { get; set; }

    public Survey Survey { get; set; }
    public Guid SurveyId { get; set; }

    public ICollection<Result> Results { get; set; }

    public DateTime StartDate { get; set; }
}
