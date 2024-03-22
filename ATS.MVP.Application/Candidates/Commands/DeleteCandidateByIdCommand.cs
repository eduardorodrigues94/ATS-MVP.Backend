using MediatR;

namespace ATS.MVP.Application.Candidates.Commands;

public record DeleteCandidateByIdCommand(Guid Id) : IRequest;
