using ATS.MVP.Domain.Candidates;
using ATS.MVP.Domain.Candidates.DTOs;
using ATS.MVP.Domain.Candidates.Errors;
using ATS.MVP.Domain.Candidates.Repositories;
using ATS.MVP.Domain.Candidates.ValueObjects;
using ATS.MVP.Domain.Common.Errors;
using ATS.MVP.Domain.Common.Models.ValueObjects;
using MediatR;

namespace ATS.MVP.Application.Candidates.Commands.Handlers;

public sealed class CreateCandidateCommandHandler : IRequestHandler<CreateCandidateCommand, Candidate>
{
    private readonly ICandidateRepository _candidatesRepository;

    public CreateCandidateCommandHandler(ICandidateRepository candidatesRepository)
    {
        _candidatesRepository = candidatesRepository;
    }

    public async Task<Candidate> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
    {
        var candidate = new Candidate(
            CandidateId.CreateUnique(),
            PersonName.Create(request.Name),
            Email.Create(request.Email),
            PhoneNumber.Create(request.PhoneNumber)
        );

        var existsByPhoneNumber = await _candidatesRepository.IsPhoneNumberDuplicated(candidate.PhoneNumber, cancellationToken);

        if (existsByPhoneNumber)
        {
            throw new DomainException(CandidatesErrorMessages.AlreadyExistsByPhoneNumber(candidate.PhoneNumber));
        }

        var existsByEmail = await _candidatesRepository.IsEmailDuplicated(candidate.Email, cancellationToken);

        if (existsByEmail)
        {
            throw new DomainException(CandidatesErrorMessages.AlreadyExistsByEmail(candidate.Email));
        }

        var dbCandidate = CandidateDTO.Create(candidate.Id.Value, request.Name, request.Email, request.PhoneNumber);

        await _candidatesRepository.AddCandidateAsync(dbCandidate, cancellationToken);

        return candidate;
    }
}
