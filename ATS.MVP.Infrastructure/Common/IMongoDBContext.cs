using ATS.MVP.Domain.Candidates.DTOs;
using MongoDB.Driver;

namespace ATS.MVP.Infrastructure.Common;

public interface IMongoDBContext
{
    IMongoCollection<CandidateDTO> Candidates { get; }
}
