using FluentValidation;
using TeamCounter.Application.Dtos;

namespace TeamCounter.Application.Validators;

public class CounterCreateDtoValidator : AbstractValidator<CounterCreateDto>
{
    public CounterCreateDtoValidator()
    {
        // check if Name is not longer then 50 chars
        RuleFor(x => x.Name)
            .MaximumLength(50);
        
        // check if Name is not empty
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Counter name is required.");
        
        // check if input value is not a default
        RuleFor(x => x.Name)
            .Must(name => !name.Equals("string", StringComparison.InvariantCultureIgnoreCase))
            .WithMessage("Counter name cannot be the default placeholder value 'string'.");
    }
}