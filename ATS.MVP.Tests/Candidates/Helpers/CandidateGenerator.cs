using ATS.MVP.Domain.Candidates;
using ATS.MVP.Domain.Candidates.ValueObjects;
using ATS.MVP.Domain.Common.Models.ValueObjects;
using Bogus;

namespace ATS.MVP.Tests.Candidates.Helpers;

public class CandidateGenerator : Faker<Candidate>
{
    public CandidateGenerator()
    {
        RuleFor(c => c.Id, f => CandidateId.Create(f.Random.Guid()));
        RuleFor(c => c.Name, f => PersonName.Create($"{f.Person.FirstName} {f.Person.LastName}"));
        RuleFor(c => c.Email, f => Email.Create(f.Internet.Email()));
        RuleFor(c => c.PhoneNumber, f => PhoneNumber.Create(f.Phone.PhoneNumber("+## (##) #####-####")));
    }
}
