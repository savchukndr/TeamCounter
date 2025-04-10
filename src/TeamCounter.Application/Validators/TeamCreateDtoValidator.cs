using FluentValidation;
using TeamCounter.Application.Dtos;

namespace TeamCounter.Application.Validators;

public class TeamCreateDtoValidator : AbstractValidator<TeamCreateDto>
{
    public TeamCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(50);
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Team name is required.");
        
        RuleFor(x => x.Name)
            .Must(name => !name.Equals("string", StringComparison.InvariantCultureIgnoreCase))
            .WithMessage("Team name cannot be the default placeholder value 'string'.");
    }
}