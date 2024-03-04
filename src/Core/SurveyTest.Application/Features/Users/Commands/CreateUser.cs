using SurveyTest.Application.Abstraction;
using MediatR;
using SurveyTest.Domain;
using AutoMapper;
using FluentValidation;
using SurveyTest.Application.Features.Users.Dto;

namespace SurveyTest.Application.Features.Users.Commands;

public record CreateUserCommand(string Name) 
    : IRequest<UserDto>;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(20);
    }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User user = new()
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };

        await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UserDto>(user);
    }
}
