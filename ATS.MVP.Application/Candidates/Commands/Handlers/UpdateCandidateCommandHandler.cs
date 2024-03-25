using ATS.MVP.Application.Candidates.Mappers;
using ATS.MVP.Domain.Candidates;
using ATS.MVP.Domain.Candidates.Errors;
using ATS.MVP.Domain.Candidates.Repositories;
using ATS.MVP.Domain.Candidates.ValueObjects;
using ATS.MVP.Domain.Common.Errors;
using ATS.MVP.Domain.Common.Models.ValueObjects;
using MediatR;

namespace ATS.MVP.Application.Candidates.Commands.Handlers;

public class UpdateCandidateCommandHandler : IRequestHandler<UpdateCandidateCommand, Candidate>
{
    private readonly ICandidateRepository _candidateRepository;

    public UpdateCandidateCommandHandler(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public async Task<Candidate> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
    {
        var candidate = new Candidate(
            CandidateId.Create(request.Id),
            PersonName.Create(request.Name),
            Email.Create(request.Email),
            PhoneNumber.Create(request.PhoneNumber)
        );

        var exists = await _candidateRepository.Exists(candidate.Id, cancellationToken);

        if (!exists)
        {
            throw new DomainException(CandidatesErrorMessages.NotFound(candidate.Id));
        }

        exists = await _candidateRepository.HasSomeOtherUserWithSameEmail(candidate.Email, candidate.Id, cancellationToken);

        if (exists)
        {
            throw new DomainException(CandidatesErrorMessages.AlreadyExistsByEmail(candidate.Email));
        }

        exists = await _candidateRepository.HasSomeOtherUserWithSamePhoneNumber(candidate.PhoneNumber, candidate.Id, cancellationToken);

        if (exists)
        {
            throw new DomainException(CandidatesErrorMessages.AlreadyExistsByPhoneNumber(candidate.PhoneNumber));
        }

        await _candidateRepository.UpdateCandidateAsync(
            CandidateId.Create(request.Id),
            candidate.ToCandidateDTO(),
            cancellationToken
        );

        return candidate;
    }
}
