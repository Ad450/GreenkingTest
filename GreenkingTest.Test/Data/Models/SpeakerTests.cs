using FluentAssertions;
using GreenkingTest.Api.Data.Models;

namespace GreenkingTest.Test.Data.Models;

public class SpeakerTests
{
    [Fact]
    public void Speaker_Properties_ShouldBeSetCorrectly()
    {
        // Arrange
        var speaker = new Speaker
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Employer = "Microsoft",
            Experience = 5,
            RegistrationFee = 100,
            Blog = new Blog { Url = "https://test.com" },
            Certifications = new List<Certification>(),
            Sessions = new List<Session>()
        };

        // Assert
        speaker.Id.Should().Be(1);
        speaker.FirstName.Should().Be("John");
        speaker.LastName.Should().Be("Doe");
        speaker.Email.Should().Be("john.doe@example.com");
        speaker.Employer.Should().Be("Microsoft");
        speaker.Experience.Should().Be(5);
        speaker.RegistrationFee.Should().Be(100);
        speaker.Blog.Should().NotBeNull();
        speaker.Certifications.Should().NotBeNull();
        speaker.Sessions.Should().NotBeNull();
    }

    [Fact]
    public void Speaker_WithNullOptionalProperties_ShouldBeValid()
    {
        // Arrange
        var speaker = new Speaker
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Employer = "Microsoft",
            Experience = null,
            RegistrationFee = 0,
            Blog = null,
            Certifications = null,
            Sessions = null
        };

        // Assert
        speaker.Id.Should().Be(1);
        speaker.FirstName.Should().Be("John");
        speaker.LastName.Should().Be("Doe");
        speaker.Email.Should().Be("john.doe@example.com");
        speaker.Employer.Should().Be("Microsoft");
        speaker.Experience.Should().BeNull();
        speaker.RegistrationFee.Should().Be(0);
        speaker.Blog.Should().BeNull();
        speaker.Certifications.Should().BeNull();
        speaker.Sessions.Should().BeNull();
    }

    [Fact]
    public void Speaker_WithDefaultValues_ShouldHaveExpectedDefaults()
    {
        // Arrange
        var speaker = new Speaker();

        // Assert
        speaker.Id.Should().Be(0);
        speaker.FirstName.Should().BeNull();
        speaker.LastName.Should().BeNull();
        speaker.Email.Should().BeNull();
        speaker.Employer.Should().BeNull();
        speaker.Experience.Should().BeNull();
        speaker.RegistrationFee.Should().Be(0);
        speaker.Blog.Should().BeNull();
        speaker.Certifications.Should().BeNull();
        speaker.Sessions.Should().BeNull();
    }
}
