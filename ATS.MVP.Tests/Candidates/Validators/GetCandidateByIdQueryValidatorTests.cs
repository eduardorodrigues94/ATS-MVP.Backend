using ATS.MVP.Application.Candidates.Queries;
using ATS.MVP.Application.Candidates.Queries.Validations;
using ATS.MVP.Domain.Candidates.Errors;
using ATS.MVP.Domain.Candidates.ValueObjects;
using Bogus;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace ATS.MVP.Tests.Candidates.Validators;

public class GetCandidateByIdQueryValidatorTests
{
    private readonly GetCandidateByIdQueryValidator _validator;

    public GetCandidateByIdQueryValidatorTests()
    {
        _validator = new GetCandidateByIdQueryValidator();
    }

    [Fact]
    public void Id_Should_Have_Error_When_Empty_Or_Null()
    {

        // Arrange
        var id = Guid.Empty;
        var command = new GetCandidateByIdQuery(id);
        var errorMessage = CandidatesErrorMessages.IdShouldBeNotEmpty(CandidateId.Create(id));

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().Be(false);

        result.Errors.Should().Contain(x => x.ErrorMessage == errorMessage);
    }

    [Fact]
    public void Id_Should_Not_Have_Error_When_Not_Empty()
    {
        // Arrange
        var faker = new Faker();
        var command = new GetCandidateByIdQuery(faker.Random.Guid());

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.IsValid.Should().Be(true);
    }
}
