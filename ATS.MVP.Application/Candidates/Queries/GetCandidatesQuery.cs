using ATS.MVP.Domain.Candidates;
using MediatR;

namespace ATS.MVP.Application.Candidates.Queries
{
    public sealed record GetCandidatesQuery : IRequest<IEnumerable<Candidate>>;
}
