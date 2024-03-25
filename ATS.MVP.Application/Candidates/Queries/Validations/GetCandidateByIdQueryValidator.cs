using ATS.MVP.Domain.Candidates.Errors;
using ATS.MVP.Domain.Candidates.ValueObjects;
using FluentValidation;

namespace ATS.MVP.Application.Candidates.Queries.Validations;

public class GetCandidateByIdQueryValidator : AbstractValidator<GetCandidateByIdQuery>
{
    public GetCandidateByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(x => CandidatesErrorMessages.IdShouldBeNotEmpty(CandidateId.Create(x.Id)));
    }
}
