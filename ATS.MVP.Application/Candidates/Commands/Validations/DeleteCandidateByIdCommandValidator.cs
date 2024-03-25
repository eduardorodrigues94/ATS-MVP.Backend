using ATS.MVP.Domain.Candidates.Errors;
using ATS.MVP.Domain.Candidates.ValueObjects;
using FluentValidation;

namespace ATS.MVP.Application.Candidates.Commands.Validations;

public class DeleteCandidateByIdCommandValidator : AbstractValidator<DeleteCandidateByIdCommand>
{
    public DeleteCandidateByIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(x => CandidatesErrorMessages.IdShouldBeNotEmpty(CandidateId.Create(x.Id)));
    }
}
