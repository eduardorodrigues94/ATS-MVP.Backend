namespace ATS.MVP.Domain.Candidates.DTOs;

public sealed class CandidateDTO
{
    private CandidateDTO(Guid id, string name, string email, string phoneNumber)
    {
        Id = id;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public static CandidateDTO Create(Guid id, string name, string email, string phoneNumber)
    {
        return new CandidateDTO(id, name, email, phoneNumber);
    }

    public CandidateDTO() { }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
