namespace SurveyTest.Domain.Exceptions;

public class SameEntityAlreadyExistException : Exception
{
    public SameEntityAlreadyExistException(string name)
        : base($"Entity {name} with same parameters already exist") { }
}
