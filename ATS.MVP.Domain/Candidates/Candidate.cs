using ATS.MVP.Domain.Candidates.ValueObjects;
using ATS.MVP.Domain.Common.Models;
using ATS.MVP.Domain.Common.ValueObjects;

namespace ATS.MVP.Domain.Candidates;

public sealed class Candidate : AggregateRoot<CandidateId>
{
    public Candidate(CandidateId id, PersonName name, Email email, PhoneNumber phoneNumber) : base(id)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public PersonName Name { get; }

    public Email Email { get; }

    public PhoneNumber PhoneNumber { get; }
}
