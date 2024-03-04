namespace SurveyTest.Application.Features.Surveys.Dto;

public record CreateQuestionDto(
    string Text,
    CreateAnswerDto[] Answers
);
