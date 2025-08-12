using FluentAssertions;
using GreenkingTest.Api.Data.Models;
using GreenkingTest.Api.Utils;

namespace GreenkingTest.Test.Utils;

public class SessionTopicCheckerTests
{
    private readonly SessionTopicChecker _checker;

    public SessionTopicCheckerTests()
    {
        _checker = new SessionTopicChecker();
    }

    [Fact]
    public void IsAllowedTopic_WithEmptySessions_ReturnsFalse()
    {
        // Arrange
        var sessions = new List<Session>();

        // Act
        var result = _checker.IsAllowedTopic(sessions);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsAllowedTopic_WithAllowedTopic_ReturnsTrue()
    {
        // Arrange
        var sessions = new List<Session>
        {
            new() { Title = "Modern C# Development", Description = "Learn the latest C# features" }
        };

        // Act
        var result = _checker.IsAllowedTopic(sessions);

        // Assert
        result.Should().BeTrue();
        sessions[0].IsApproved.Should().BeTrue();
    }

    [Fact]
    public void IsAllowedTopic_WithForbiddenTopicCobol_ReturnsFalse()
    {
        // Arrange
        var sessions = new List<Session>
        {
            new() { Title = "Introduction to Cobol", Description = "Learn about old technology" }
        };

        // Act
        var result = _checker.IsAllowedTopic(sessions);

        // Assert
        result.Should().BeFalse();
        sessions[0].IsApproved.Should().BeFalse();
    }

    [Fact]
    public void IsAllowedTopic_WithForbiddenTopicPunchCards_ReturnsFalse()
    {
        // Arrange
        var sessions = new List<Session>
        {
            new() { Title = "Introduction to Punch Cards", Description = "Learn about old technology" }
        };

        // Act
        var result = _checker.IsAllowedTopic(sessions);

        // Assert
        result.Should().BeFalse();
        sessions[0].IsApproved.Should().BeFalse();
    }

    [Fact]
    public void IsAllowedTopic_WithForbiddenTopicCommodore_ReturnsFalse()
    {
        // Arrange
        var sessions = new List<Session>
        {
            new() { Title = "Introduction to Commodore", Description = "Learn about old technology" }
        };

        // Act
        var result = _checker.IsAllowedTopic(sessions);

        // Assert
        result.Should().BeFalse();
        sessions[0].IsApproved.Should().BeFalse();
    }

    [Fact]
    public void IsAllowedTopic_WithForbiddenTopicVBScript_ReturnsFalse()
    {
        // Arrange
        var sessions = new List<Session>
        {
            new() { Title = "Introduction to VBScript", Description = "Learn about old technology" }
        };

        // Act
        var result = _checker.IsAllowedTopic(sessions);

        // Assert
        result.Should().BeFalse();
        sessions[0].IsApproved.Should().BeFalse();
    }

    [Fact]
    public void IsAllowedTopic_WithMixedTopics_ReturnsTrueIfAnyAllowed()
    {
        // Arrange
        var sessions = new List<Session>
        {
            new() { Title = "Introduction to Cobol", Description = "Old language" },
            new() { Title = "Modern C# Development", Description = "New language" }
        };

        // Act
        var result = _checker.IsAllowedTopic(sessions);

        // Assert
        result.Should().BeTrue();
        sessions[0].IsApproved.Should().BeFalse();
        sessions[1].IsApproved.Should().BeTrue();
    }

    [Fact]
    public void IsAllowedTopic_WithForbiddenTopicInDescription_ReturnsFalse()
    {
        // Arrange
        var sessions = new List<Session>
        {
            new() { Title = "Programming Basics", Description = "Learn programming with VBScript and modern languages" }
        };

        // Act
        var result = _checker.IsAllowedTopic(sessions);

        // Assert
        result.Should().BeFalse();
        sessions[0].IsApproved.Should().BeFalse();
    }

    [Fact]
    public void IsAllowedTopic_WithCaseInsensitiveForbiddenTopic_ReturnsFalse()
    {
        // Arrange
        var sessions = new List<Session>
        {
            new() { Title = "Introduction to COBOL", Description = "Learn about old language" }
        };

        // Act
        var result = _checker.IsAllowedTopic(sessions);

        // Assert
        result.Should().BeFalse();
        sessions[0].IsApproved.Should().BeFalse();
    }
}
