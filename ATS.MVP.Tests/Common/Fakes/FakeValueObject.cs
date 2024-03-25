using ATS.MVP.Domain.Common.Models;

namespace ATS.MVP.Tests.Common.Fakes;

public class FakeValueObject : ValueObject
{
    private readonly string _value;

    public FakeValueObject(string value)
    {
        _value = value;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }
}
