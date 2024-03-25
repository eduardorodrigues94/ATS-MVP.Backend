using ATS.MVP.Domain.Common.Models;

namespace ATS.MVP.Tests.Common.Fakes;
public class FakeEntity : Entity<int>
{
    public FakeEntity(int id) : base(id)
    {
    }
}
