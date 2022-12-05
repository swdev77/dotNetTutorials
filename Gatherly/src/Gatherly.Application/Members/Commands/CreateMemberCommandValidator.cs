using FluentValidation;
using Gatherly.Domain.ValueObjects;

namespace Gatherly.Application.Members.Commands;

public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
{
    public CreateMemberCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(FirstName.MaxLength);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(LastName.MaxLength);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}