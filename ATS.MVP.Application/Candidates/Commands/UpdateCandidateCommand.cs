using ATS.MVP.Domain.Candidates;
using MediatR;

namespace ATS.MVP.Application.Candidates.Commands;

public record UpdateCandidateCommand(Guid Id, string Name, string Email, string PhoneNumber) : IRequest<Candidate>;
