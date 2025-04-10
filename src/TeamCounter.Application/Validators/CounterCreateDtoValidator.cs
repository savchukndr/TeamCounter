using FluentValidation;
using TeamCounter.Application.Dtos;

namespace TeamCounter.Application.Validators;

public class CounterCreateDtoValidator : AbstractValidator<CounterCreateDto>
{
    public CounterCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(50);
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Counter name is required.");
        
        RuleFor(x => x.Name)
            .Must(name => !name.Equals("string", StringComparison.InvariantCultureIgnoreCase))
            .WithMessage("Counter name cannot be the default placeholder value 'string'.");
    }
}