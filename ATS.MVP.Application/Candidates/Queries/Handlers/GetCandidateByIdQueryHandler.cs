using ATS.MVP.Domain.Candidates;
using ATS.MVP.Domain.Candidates.Errors;
using ATS.MVP.Domain.Candidates.Repositories;
using ATS.MVP.Domain.Candidates.ValueObjects;
using ATS.MVP.Domain.Common.Errors;
using ATS.MVP.Domain.Common.Models.ValueObjects;
using MediatR;

namespace ATS.MVP.Application.Candidates.Queries.Handlers;

public class GetCandidateByIdQueryHandler : IRequestHandler<GetCandidateByIdQuery, Candidate>
{
    private readonly ICandidateRepository _candidateRepository;

    public GetCandidateByIdQueryHandler(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public async Task<Candidate> Handle(GetCandidateByIdQuery request, CancellationToken cancellationToken)
    {
        var candidateId = CandidateId.Create(request.Id);

        var dto = await _candidateRepository.GetCandidateByIdAsync(candidateId, cancellationToken);

        if (dto is null)
        {
            throw new DomainException(CandidatesErrorMessages.NotFound(candidateId), 404);
        }

        return new Candidate(candidateId, PersonName.Create(dto.Name), Email.Create(dto.Email), PhoneNumber.Create(dto.PhoneNumber));
    }
}
