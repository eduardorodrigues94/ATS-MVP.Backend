using ATS.MVP.Domain.Candidates;
using ATS.MVP.Domain.Candidates.DTOs;
using ATS.MVP.Domain.Candidates.ValueObjects;
using ATS.MVP.Domain.Common.Models.ValueObjects;

namespace ATS.MVP.Application.Candidates.Mappers;

public static class CandidateMapper
{
    public static CandidateDTO FromCandidate(this Candidate candidate)
    {
        return CandidateDTO.Create(
            candidate.Id.Value,
            candidate.Name.Value,
            candidate.Email.Value,
            candidate.PhoneNumber.Normalized()
        );
    }

    public static Candidate FromCandidateDTO(this CandidateDTO candidateDTO)
    {
        return new Candidate(
            CandidateId.Create(candidateDTO.Id),
            PersonName.Create(candidateDTO.Name),
            Email.Create(candidateDTO.Email),
            PhoneNumber.Create(candidateDTO.PhoneNumber)
        );
    }

    public static CandidateDTO ToCandidateDTO(this Candidate candidate)
    {
        return CandidateDTO.Create(
            candidate.Id.Value,
            candidate.Name.Value,
            candidate.Email.Value,
            candidate.PhoneNumber.Normalized()
        );
    }

    public static Candidate ToCandidate(this CandidateDTO candidateDTO)
    {
        return new Candidate(
          CandidateId.Create(candidateDTO.Id),
          PersonName.Create(candidateDTO.Name),
          Email.Create(candidateDTO.Email),
          PhoneNumber.Create(candidateDTO.PhoneNumber)
      );
    }
}
