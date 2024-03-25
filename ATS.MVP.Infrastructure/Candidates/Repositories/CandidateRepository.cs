using ATS.MVP.Domain.Candidates.DTOs;
using ATS.MVP.Domain.Candidates.Repositories;
using ATS.MVP.Domain.Candidates.ValueObjects;
using ATS.MVP.Domain.Common.Models.ValueObjects;
using ATS.MVP.Infrastructure.Common;
using MongoDB.Driver;

namespace ATS.MVP.Infrastructure.Candidates.Repositories;

public sealed class CandidateRepository : ICandidateRepository
{
    private readonly IMongoDBContext _context;

    public CandidateRepository(IMongoDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CandidateDTO>> GetAllCandidatesAsync(CancellationToken cancellationToken)
    {
        return await _context.Candidates.Find(_ => true).ToListAsync(cancellationToken);
    }

    public async Task<CandidateDTO> GetCandidateByIdAsync(CandidateId id, CancellationToken cancellationToken)
    {
        return await _context.Candidates.Find(c => c.Id == id.Value).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> IsEmailDuplicated(Email email, CancellationToken cancellationToken)
    {
        return await _context
            .Candidates
            .Find(u => u.Email == email.Value)
            .AnyAsync(cancellationToken);
    }

    public async Task<bool> IsPhoneNumberDuplicated(PhoneNumber phoneNumber, CancellationToken cancellationToken)
    {
        return await _context
            .Candidates
            .Find(u => u.PhoneNumber == phoneNumber.Normalized())
            .AnyAsync(cancellationToken);
    }

    public async Task AddCandidateAsync(CandidateDTO candidate, CancellationToken cancellationToken)
    {
        await _context.Candidates.InsertOneAsync(candidate, cancellationToken: cancellationToken);
    }

    public async Task<bool> UpdateCandidateAsync(CandidateId id, CandidateDTO candidate, CancellationToken cancellationToken)
    {
        var updateResult = await _context.Candidates.ReplaceOneAsync(c => c.Id == id.Value, candidate, cancellationToken: cancellationToken);

        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteCandidateAsync(CandidateId id, CancellationToken cancellationToken)
    {
        var deleteResult = await _context.Candidates.DeleteOneAsync(c => c.Id == id.Value, cancellationToken);

        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }

    public Task<bool> Exists(CandidateId id, CancellationToken cancellationToken)
    {
        return _context
            .Candidates
            .Find(u => u.Id == id.Value)
            .AnyAsync(cancellationToken);
    }

    public Task<bool> HasSomeOtherUserWithSameEmail(Email email, CandidateId id, CancellationToken cancellationToken)
    {
        return _context
          .Candidates
          .Find(u => u.Id != id.Value && u.Email == email.Value)
          .AnyAsync(cancellationToken);
    }

    public Task<bool> HasSomeOtherUserWithSamePhoneNumber(PhoneNumber phoneNumber, CandidateId id, CancellationToken cancellationToken)
    {
        return _context
           .Candidates
           .Find(u => u.Id != id.Value && u.PhoneNumber == phoneNumber.Normalized())
           .AnyAsync(cancellationToken);
    }
}
