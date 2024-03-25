using ATS.MVP.Domain.Candidates.DTOs;
using Bogus;

namespace ATS.MVP.Tests.Candidates.Helpers;

public class CandidateDTOGenerator : Faker<CandidateDTO>
{
    public CandidateDTOGenerator()
    {
        RuleFor(c => c.Id, f => f.Random.Guid());

        RuleFor(c => c.Name, f => $"{f.Person.FirstName} {f.Person.LastName}");

        RuleFor(c => c.Email, f => f.Internet.Email());

        RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber("+## (##) #####-####"));
    }
}
