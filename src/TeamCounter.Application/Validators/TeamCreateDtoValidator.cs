using FluentValidation;
using TeamCounter.Application.Dtos;

namespace TeamCounter.Application.Validators;

public class TeamCreateDtoValidator : AbstractValidator<TeamCreateDto>
{
    public TeamCreateDtoValidator()
    {
        // check if Name is not longer then 50 chars
        RuleFor(x => x.Name)
            .MaximumLength(50);
        
        // check if Name is not empty
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Team name is required.");
        
        // check if input value is not a default
        RuleFor(x => x.Name)
            .Must(name => !name.Equals("string", StringComparison.InvariantCultureIgnoreCase))
            .WithMessage("Team name cannot be the default placeholder value 'string'.");
    }
}