using ATS.MVP.Domain.Candidates.DTOs;
using ATS.MVP.Domain.Candidates.ValueObjects;
using ATS.MVP.Domain.Common.ValueObjects;

namespace ATS.MVP.Domain.Candidates.Repositories;

public interface ICandidateRepository
{
    Task AddCandidateAsync(CandidateDTO candidate, CancellationToken cancellationToken);
    Task<bool> IsEmailDuplicated(Email email, CancellationToken cancellationToken);
    Task<bool> IsPhoneNumberDuplicated(PhoneNumber phoneNumber, CancellationToken cancellationToken);
    Task<bool> HasSomeOtherUserWithSameEmail(Email email, CandidateId id, CancellationToken cancellationToken);
    Task<bool> HasSomeOtherUserWithSamePhoneNumber(PhoneNumber phoneNumber, CandidateId id, CancellationToken cancellationToken);
    Task<bool> DeleteCandidateAsync(CandidateId id, CancellationToken cancellationToken);
    Task<bool> Exists(CandidateId id, CancellationToken cancellationToken);
    Task<IEnumerable<CandidateDTO>> GetAllCandidatesAsync(CancellationToken cancellationToken);
    Task<CandidateDTO> GetCandidateByIdAsync(CandidateId id, CancellationToken cancellationToken);
    Task<bool> UpdateCandidateAsync(CandidateId id, CandidateDTO candidate, CancellationToken cancellationToken);
}
