using ATS.MVP.Domain.Candidates.Repositories;
using ATS.MVP.Domain.Candidates.ValueObjects;
using MediatR;

namespace ATS.MVP.Application.Candidates.Commands.Handlers;

internal sealed class DeleteCandidateByIdCommandHandler : IRequestHandler<DeleteCandidateByIdCommand>
{
    private readonly ICandidateRepository _candidateRepository;

    public DeleteCandidateByIdCommandHandler(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public Task Handle(DeleteCandidateByIdCommand request, CancellationToken cancellationToken)
    {
        return _candidateRepository.DeleteCandidateAsync(CandidateId.Create(request.Id), cancellationToken);
    }
}
