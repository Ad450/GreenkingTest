using FluentAssertions;
using GreenkingTest.Api.Utils;

namespace GreenkingTest.Test.Utils;

public class DomainCheckerTests
{
    private readonly DomainChecker _checker;

    public DomainCheckerTests()
    {
        _checker = new DomainChecker();
    }

    [Fact]
    public void IsAllowedDomain_WithForbiddenDomainAol_ReturnsFalse()
    {
        // Act
        var result = _checker.IsAllowedDomain("user@aol.com");

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsAllowedDomain_WithForbiddenDomainProdigy_ReturnsFalse()
    {
        // Act
        var result = _checker.IsAllowedDomain("user@prodigy.com");

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsAllowedDomain_WithForbiddenDomainCompuserve_ReturnsFalse()
    {
        // Act
        var result = _checker.IsAllowedDomain("user@compuserve.com");

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsAllowedDomain_WithAllowedDomainGmail_ReturnsTrue()
    {
        // Act
        var result = _checker.IsAllowedDomain("user@gmail.com");

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsAllowedDomain_WithAllowedDomainMicrosoft_ReturnsTrue()
    {
        // Act
        var result = _checker.IsAllowedDomain("user@microsoft.com");

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsAllowedDomain_WithAllowedDomainOutlook_ReturnsTrue()
    {
        // Act
        var result = _checker.IsAllowedDomain("user@outlook.com");

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsAllowedDomain_WithEmptyEmail_ReturnsFalse()
    {
        // Act
        var result = _checker.IsAllowedDomain("");

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsAllowedDomain_WithInvalidEmail_ReturnsFalse()
    {
        // Act
        var result = _checker.IsAllowedDomain("invalid-email");

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsAllowedDomain_WithNoAtSymbol_ReturnsFalse()
    {
        // Act
        var result = _checker.IsAllowedDomain("no-at-symbol");

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsAllowedDomain_WithAtSymbolOnly_ReturnsTrue()
    {
        // Act
        var result = _checker.IsAllowedDomain("@nodomain.com");

        // Assert
        result.Should().BeTrue(); // Domain is "nodomain.com" which is not forbidden
    }

    [Fact]
    public void IsAllowedDomain_WithUserOnly_ReturnsTrue()
    {
        // Act
        var result = _checker.IsAllowedDomain("user@");

        // Assert
        result.Should().BeTrue(); // Domain is "" (empty string) which is not forbidden
    }

    [Fact]
    public void IsAllowedDomain_WithMixedCaseDomain_ReturnsTrue()
    {
        // Arrange
        var email = "user@AOL.COM";

        // Act
        var result = _checker.IsAllowedDomain(email);

        // Assert
        result.Should().BeTrue(); // Domain is "AOL.COM" which is not in the forbidden list (case-sensitive)
    }
}