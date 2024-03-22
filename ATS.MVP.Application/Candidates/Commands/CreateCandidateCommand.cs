using ATS.MVP.Domain.Candidates;
using MediatR;

namespace ATS.MVP.Application.Candidates.Commands;

public record CreateCandidateCommand(string Name, string Email, string PhoneNumber) : IRequest<Candidate>;
