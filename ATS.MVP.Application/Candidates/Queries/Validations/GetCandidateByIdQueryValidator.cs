using FluentValidation;

namespace ATS.MVP.Application.Candidates.Queries.Validations
{
    public class GetCandidateByIdQueryValidator : AbstractValidator<GetCandidateByIdQuery>
    {
        public GetCandidateByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id não pode ser nulo");
        }
    }
}
