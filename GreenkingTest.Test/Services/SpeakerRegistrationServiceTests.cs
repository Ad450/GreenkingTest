using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using GreenkingTest.Api.Data.Enums;
using GreenkingTest.Api.Data.Models;
using GreenkingTest.Api.DataTransferObjects;
using GreenkingTest.Api.Repositories;
using GreenkingTest.Api.Services;
using GreenkingTest.Api.Utils;
using Moq;

namespace GreenkingTest.Test.Services;

public class SpeakerRegistrationServiceTests
{
    private readonly Mock<IValidator<SpeakerDto>> _mockValidator;
    private readonly Mock<IEmployerChecker> _mockEmployerChecker;
    private readonly Mock<IDomainChecker> _mockDomainChecker;
    private readonly Mock<ISessionTopicChecker> _mockSessionTopicChecker;
    private readonly Mock<ISpeakerRepository> _mockSpeakerRepository;
    private readonly SpeakerRegistrationService _service;

    public SpeakerRegistrationServiceTests()
    {
        _mockValidator = new Mock<IValidator<SpeakerDto>>();
        _mockEmployerChecker = new Mock<IEmployerChecker>();
        _mockDomainChecker = new Mock<IDomainChecker>();
        _mockSessionTopicChecker = new Mock<ISessionTopicChecker>();
        _mockSpeakerRepository = new Mock<ISpeakerRepository>();
        
        _service = new SpeakerRegistrationService(
            _mockValidator.Object,
            _mockEmployerChecker.Object,
            _mockDomainChecker.Object,
            _mockSessionTopicChecker.Object,
            _mockSpeakerRepository.Object
        );
    }

    [Fact]
    public async Task RegisterSpeaker_WithValidSpeaker_ReturnsSuccessResponse()
    {
        // Arrange
        var speakerDto = CreateValidSpeakerDto();
        var validationResult = new ValidationResult();
        
        _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<SpeakerDto>(), default))
            .ReturnsAsync(validationResult);
        _mockEmployerChecker.Setup(x => x.IsAllowedEmployer(It.IsAny<string>()))
            .Returns(true);
        _mockSessionTopicChecker.Setup(x => x.IsAllowedTopic(It.IsAny<IList<Session>>()))
            .Returns(true);
        _mockSpeakerRepository.Setup(x => x.SaveSpeaker(It.IsAny<Speaker>()))
            .ReturnsAsync("speaker-123");

        // Act
        var result = await _service.RegisterSpeaker(speakerDto);

        // Assert
        result.Success.Should().BeTrue();
        result.Data.Should().Be("speaker-123");
        result.Error.Should().BeNull();
    }

    [Fact]
    public async Task RegisterSpeaker_WithValidationFailure_ReturnsErrorResponse()
    {
        // Arrange
        var speakerDto = CreateValidSpeakerDto();
        var validationResult = new ValidationResult(new[] 
        { 
            new ValidationFailure("FirstName", "First name is required.") 
        });
        
        _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<SpeakerDto>(), default))
            .ReturnsAsync(validationResult);

        // Act
        var result = await _service.RegisterSpeaker(speakerDto);

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Should().Be("First name is required.");
        result.Data.Should().BeNull();
    }

    [Fact]
    public async Task RegisterSpeaker_DoesNotMeetMinimumStandards_ReturnsErrorResponse()
    {
        // Arrange
        var speakerDto = CreateSpeakerThatDoesNotMeetStandards();
        var validationResult = new ValidationResult();
        
        _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<SpeakerDto>(), default))
            .ReturnsAsync(validationResult);
        _mockEmployerChecker.Setup(x => x.IsAllowedEmployer(It.IsAny<string>()))
            .Returns(false);
        _mockDomainChecker.Setup(x => x.IsAllowedDomain(It.IsAny<string>()))
            .Returns(false);

        // Act
        var result = await _service.RegisterSpeaker(speakerDto);

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Should().Be("Speaker does not meet standards");
    }

    [Fact]
    public async Task RegisterSpeaker_SessionNotApproved_ReturnsErrorResponse()
    {
        // Arrange
        var speakerDto = CreateValidSpeakerDto();
        var validationResult = new ValidationResult();
        
        _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<SpeakerDto>(), default))
            .ReturnsAsync(validationResult);
        _mockEmployerChecker.Setup(x => x.IsAllowedEmployer(It.IsAny<string>()))
            .Returns(true);
        _mockSessionTopicChecker.Setup(x => x.IsAllowedTopic(It.IsAny<IList<Session>>()))
            .Returns(false);

        // Act
        var result = await _service.RegisterSpeaker(speakerDto);

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Should().Be("Session not Approved");
    }

    [Fact]
    public async Task RegisterSpeaker_WithZeroExperience_CalculatesCorrectRegistrationFee()
    {
        await TestRegistrationFeeCalculation(0, 500);
    }

    [Fact]
    public async Task RegisterSpeaker_WithOneYearExperience_CalculatesCorrectRegistrationFee()
    {
        await TestRegistrationFeeCalculation(1, 500);
    }

    [Fact]
    public async Task RegisterSpeaker_WithTwoYearsExperience_CalculatesCorrectRegistrationFee()
    {
        await TestRegistrationFeeCalculation(2, 250);
    }

    [Fact]
    public async Task RegisterSpeaker_WithThreeYearsExperience_CalculatesCorrectRegistrationFee()
    {
        await TestRegistrationFeeCalculation(3, 250);
    }

    [Fact]
    public async Task RegisterSpeaker_WithFourYearsExperience_CalculatesCorrectRegistrationFee()
    {
        await TestRegistrationFeeCalculation(4, 100);
    }

    [Fact]
    public async Task RegisterSpeaker_WithFiveYearsExperience_CalculatesCorrectRegistrationFee()
    {
        await TestRegistrationFeeCalculation(5, 100);
    }

    [Fact]
    public async Task RegisterSpeaker_WithSixYearsExperience_CalculatesCorrectRegistrationFee()
    {
        await TestRegistrationFeeCalculation(6, 50);
    }

    [Fact]
    public async Task RegisterSpeaker_WithNineYearsExperience_CalculatesCorrectRegistrationFee()
    {
        await TestRegistrationFeeCalculation(9, 50);
    }

    [Fact]
    public async Task RegisterSpeaker_WithTenYearsExperience_CalculatesCorrectRegistrationFee()
    {
        await TestRegistrationFeeCalculation(10, 0);
    }

    [Fact]
    public async Task RegisterSpeaker_WithFifteenYearsExperience_CalculatesCorrectRegistrationFee()
    {
        await TestRegistrationFeeCalculation(15, 0);
    }

    private async Task TestRegistrationFeeCalculation(int experience, int expectedFee)
    {
        // Arrange
        var speakerDto = CreateValidSpeakerDto();
        speakerDto.Experience = experience;
        var validationResult = new ValidationResult();
        
        _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<SpeakerDto>(), default))
            .ReturnsAsync(validationResult);
        _mockEmployerChecker.Setup(x => x.IsAllowedEmployer(It.IsAny<string>()))
            .Returns(true);
        _mockSessionTopicChecker.Setup(x => x.IsAllowedTopic(It.IsAny<IList<Session>>()))
            .Returns(true);
        _mockSpeakerRepository.Setup(x => x.SaveSpeaker(It.IsAny<Speaker>()))
            .ReturnsAsync("speaker-123");

        // Act
        var result = await _service.RegisterSpeaker(speakerDto);

        // Assert
        result.Success.Should().BeTrue();
        _mockSpeakerRepository.Verify(x => x.SaveSpeaker(It.Is<Speaker>(s => s.RegistrationFee == expectedFee)), Times.Once);
    }

    private static SpeakerDto CreateValidSpeakerDto()
    {
        return new SpeakerDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@microsoft.com",
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

    private static SpeakerDto CreateSpeakerThatDoesNotMeetStandards()
    {
        return new SpeakerDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@aol.com", // Forbidden domain
            Experience = 5, // Less than 10 years
            Employer = "SmallCompany", // Not a big tech company
            Blog = null, // No blog
            Certifications = new List<CertificationDto>
            {
                new() { Name = "Cert 1" },
                new() { Name = "Cert 2" }
                // Only 2 certifications, less than 3
            },
            Sessions = new List<SessionDto>
            {
                new() { Title = "Modern C#", Description = "Learn modern C# features" }
            },
            Browser = new WebBrowserDto { Name = BrowserName.InternetExplorer, MajorVersion = 8 } // Forbidden browser
        };
    }
} 