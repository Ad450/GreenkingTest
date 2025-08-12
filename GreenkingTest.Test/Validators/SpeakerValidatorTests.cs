using FluentAssertions;
using FluentValidation.TestHelper;
using GreenkingTest.Api.Data.Enums;
using GreenkingTest.Api.DataTransferObjects;
using GreenkingTest.Api.Validators;

namespace GreenkingTest.Test.Validators;

public class SpeakerValidatorTests
{
    private readonly SpeakerValidator _validator;

    public SpeakerValidatorTests()
    {
        _validator = new SpeakerValidator();
    }

    [Fact]
    public void Validate_WithValidSpeaker_ShouldNotHaveValidationErrors()
    {
        // Arrange
        var speaker = CreateValidSpeaker();

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WithEmptyFirstName_ShouldHaveValidationError()
    {
        // Arrange
        var speaker = CreateValidSpeaker();
        speaker.FirstName = "";

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == "First name is required.");
    }

    [Fact]
    public void Validate_WithNullFirstName_ShouldHaveValidationError()
    {
        // Arrange
        var speaker = CreateValidSpeaker();
        speaker.FirstName = null;

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == "First name is required.");
    }

    [Fact]
    public void Validate_WithWhitespaceFirstName_ShouldHaveValidationError()
    {
        // Arrange
        var speaker = CreateValidSpeaker();
        speaker.FirstName = "   ";

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == "First name is required.");
    }

    [Fact]
    public void Validate_WithEmptyLastName_ShouldHaveValidationError()
    {
        // Arrange
        var speaker = CreateValidSpeaker();
        speaker.LastName = "";

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LastName);
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == "Last name is required.");
    }

    [Fact]
    public void Validate_WithNullLastName_ShouldHaveValidationError()
    {
        // Arrange
        var speaker = CreateValidSpeaker();
        speaker.LastName = null;

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LastName);
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == "Last name is required.");
    }

    [Fact]
    public void Validate_WithWhitespaceLastName_ShouldHaveValidationError()
    {
        // Arrange
        var speaker = CreateValidSpeaker();
        speaker.LastName = "   ";

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LastName);
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == "Last name is required.");
    }

    [Fact]
    public void Validate_WithEmptyEmail_ShouldHaveValidationError()
    {
        // Arrange
        var speaker = CreateValidSpeaker();
        speaker.Email = "";

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == "Email is required.");
    }

    [Fact]
    public void Validate_WithNullEmail_ShouldHaveValidationError()
    {
        // Arrange
        var speaker = CreateValidSpeaker();
        speaker.Email = null;

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == "Email is required.");
    }

    [Fact]
    public void Validate_WithWhitespaceEmail_ShouldHaveValidationError()
    {
        // Arrange
        var speaker = CreateValidSpeaker();
        speaker.Email = "   ";

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == "Email is required.");
    }

    [Fact]
    public void Validate_WithValidEmailFormat_ShouldNotHaveValidationError()
    {
        // Arrange
        var speaker = CreateValidSpeaker();
        speaker.Email = "john.doe@example.com";

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Validate_WithEmailMissingAtSymbol_ShouldHaveValidationError()
    {
        // Arrange
        var speaker = CreateValidSpeaker();
        speaker.Email = "john.doeexample.com";

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == "Invalid email format.");
    }

    [Fact]
    public void Validate_WithEmailMissingDomain_ShouldHaveValidationError()
    {
        // Arrange
        var speaker = CreateValidSpeaker();
        speaker.Email = "john.doe@";

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == "Invalid email format.");
    }

    [Fact]
    public void Validate_WithEmailMissingTopLevelDomain_ShouldHaveValidationError()
    {
        // Arrange
        var speaker = CreateValidSpeaker();
        speaker.Email = "john.doe@example";

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == "Invalid email format.");
    }

    [Fact]
    public void Validate_WithEmailContainingWhitespace_ShouldHaveValidationError()
    {
        // Arrange
        var speaker = CreateValidSpeaker();
        speaker.Email = "john doe@example.com";

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == "Invalid email format.");
    }

    [Fact]
    public void Validate_WithEmailContainingMultipleAtSymbols_ShouldHaveValidationError()
    {
        // Arrange
        var speaker = CreateValidSpeaker();
        speaker.Email = "john@doe@example.com";

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == "Invalid email format.");
    }

    [Fact]
    public void Validate_WithEmailStartingWithAtSymbol_ShouldHaveValidationError()
    {
        // Arrange
        var speaker = CreateValidSpeaker();
        speaker.Email = "@example.com";

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == "Invalid email format.");
    }

    [Fact]
    public void Validate_WithEmailEndingWithAtSymbol_ShouldHaveValidationError()
    {
        // Arrange
        var speaker = CreateValidSpeaker();
        speaker.Email = "john.doe@";

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == "Invalid email format.");
    }

    [Fact]
    public void Validate_WithEmailContainingSpecialCharacters_ShouldHaveValidationError()
    {
        // Arrange
        var speaker = CreateValidSpeaker();
        speaker.Email = "john.doe@example.com@";

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == "Invalid email format.");
    }

    [Fact]
    public void Validate_WithValidEmailWithSubdomain_ShouldNotHaveValidationError()
    {
        // Arrange
        var speaker = CreateValidSpeaker();
        speaker.Email = "john.doe@sub.example.com";

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Validate_WithValidEmailWithHyphens_ShouldNotHaveValidationError()
    {
        // Arrange
        var speaker = CreateValidSpeaker();
        speaker.Email = "john-doe@example-domain.com";

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Validate_WithValidEmailWithUnderscores_ShouldNotHaveValidationError()
    {
        // Arrange
        var speaker = CreateValidSpeaker();
        speaker.Email = "john_doe@example_domain.com";

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Validate_WithNullExperience_ShouldHaveValidationError()
    {
        // Arrange
        var speaker = CreateValidSpeaker();
        speaker.Experience = null;

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Experience);
    }

    [Fact]
    public void Validate_WithValidExperience_ShouldNotHaveValidationError()
    {
        // Arrange
        var speaker = CreateValidSpeaker();
        speaker.Experience = 5;

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Experience);
    }

    [Fact]
    public void Validate_FirstNameValidation_ShouldStopCascade()
    {
        // Arrange
        var speaker = CreateValidSpeaker();
        speaker.FirstName = ""; // This should trigger cascade stop
        speaker.LastName = "";  // This should still be validated

        // Act
        var result = _validator.TestValidate(speaker);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
        result.ShouldHaveValidationErrorFor(x => x.LastName);
        result.Errors.Should().HaveCount(2);
    }

    private static SpeakerDto CreateValidSpeaker()
    {
        return new SpeakerDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Experience = 5,
            Employer = "Microsoft",
            Blog = new BlogDto { Url = "https://test.com" },
            Certifications = new List<CertificationDto>
            {
                new() { Name = "Cert 1" },
                new() { Name = "Cert 2" },
                new() { Name = "Cert 3" },
                new() { Name = "Cert 4" }
            },
            Sessions = new List<SessionDto>
            {
                new() { Title = "Modern C#", Description = "Learn modern C# features" }
            },
            Browser = new WebBrowserDto { Name = BrowserName.Chrome, MajorVersion = 10 }
        };
    }
}
