using ATS.MVP.Domain.Common.Models;

namespace ATS.MVP.Domain.Candidates.ValueObjects;

public sealed class CandidateId : ValueObject
{
    public Guid Value { get; }

    private CandidateId(Guid value)
    {
        Value = value;
    }

    public static CandidateId CreateUnique()
    {
        return new CandidateId(Guid.NewGuid());
    }

    public static CandidateId Create(Guid id)
    {
        return new CandidateId(id);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
