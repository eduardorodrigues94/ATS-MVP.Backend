using ATS.MVP.Domain.Candidates.ValueObjects;
using ATS.MVP.Domain.Common.Models.ValueObjects;

namespace ATS.MVP.Domain.Candidates.Errors;

public static class CandidatesErrorMessages
{
    public static string IdShouldBeNotEmpty(CandidateId id) => $"'id' não pode ser null ou default";
    public static string NotFound(CandidateId id) => $"Candidato '{id.Value}' não encontrado";
    public static string AlreadyExistsByEmail(Email email) => $"Email '{email.Value}' já cadastrado";
    public static string AlreadyExistsByPhoneNumber(PhoneNumber phoneNumber) => $"Número de telefone '{phoneNumber}' já cadastrado";
}
