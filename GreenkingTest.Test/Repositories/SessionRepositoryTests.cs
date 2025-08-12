using FluentAssertions;
using GreenkingTest.Api.Data.Models;
using GreenkingTest.Api.Repositories;

namespace GreenkingTest.Test.Repositories;

public class SessionRepositoryTests
{
    [Fact]
    public async Task GetSessions_ShouldReturnEmptyCollection()
    {
        // Arrange
        var repository = new SessionRepository();

        // Act
        var result = await repository.GetSessions();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetSessions_ShouldReturnIEnumerableOfSessions()
    {
        // Arrange
        var repository = new SessionRepository();

        // Act
        var result = await repository.GetSessions();

        // Assert
        result.Should().BeAssignableTo<IEnumerable<Session>>();
    }
}
