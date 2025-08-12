using FluentAssertions;
using GreenkingTest.Api.Data.Models;
using GreenkingTest.Api.Repositories;

namespace GreenkingTest.Test.Repositories;

public class SpeakerRepositoryTests
{
    [Fact]
    public async Task SaveSpeaker_ShouldThrowNotImplementedException()
    {
        // Arrange
        var repository = new SpeakerRepository();
        var speaker = new Speaker
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com"
        };

        // Act & Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => repository.SaveSpeaker(speaker));
    }

    [Fact]
    public async Task SaveSpeaker_WithNullSpeaker_ShouldThrowNotImplementedException()
    {
        // Arrange
        var repository = new SpeakerRepository();

        // Act & Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => repository.SaveSpeaker(null!));
    }
}
