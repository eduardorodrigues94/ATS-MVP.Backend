using FluentValidation;

namespace ATS.MVP.Application.Candidates.Commands.Validations;

public class DeleteCandidateByIdCommandValidator : AbstractValidator<DeleteCandidateByIdCommand>
{
    public DeleteCandidateByIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id não pode ser nulo");
    }
}
