using ATS.MVP.Domain.Candidates;
using ATS.MVP.Domain.Candidates.Repositories;
using ATS.MVP.Domain.Candidates.ValueObjects;
using ATS.MVP.Domain.Common.ValueObjects;
using MediatR;

namespace ATS.MVP.Application.Candidates.Queries.Handlers;

internal class GetCandidatesQueryHandler : IRequestHandler<GetCandidatesQuery, IEnumerable<Candidate>>
{
    private readonly ICandidateRepository _candidateRepository;

    public GetCandidatesQueryHandler(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public async Task<IEnumerable<Candidate>> Handle(GetCandidatesQuery request, CancellationToken cancellationToken)
    {
        var list = await _candidateRepository.GetAllCandidatesAsync(cancellationToken);

        return list.Select(dto => new Candidate(CandidateId.Create(dto.Id), PersonName.Create(dto.Name), Email.Create(dto.Email), PhoneNumber.Create(dto.PhoneNumber)));
    }
}
