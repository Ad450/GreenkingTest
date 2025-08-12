using FluentAssertions;
using GreenkingTest.Api.Utils;

namespace GreenkingTest.Test.Utils;

public class EmployerCheckerTests
{
    private readonly EmployerChecker _checker;

    public EmployerCheckerTests()
    {
        _checker = new EmployerChecker();
    }

    [Fact]
    public void IsAllowedEmployer_WithAllowedEmployerPluralsight_ReturnsTrue()
    {
        // Act
        var result = _checker.IsAllowedEmployer("Pluralsight");

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsAllowedEmployer_WithAllowedEmployerMicrosoft_ReturnsTrue()
    {
        // Act
        var result = _checker.IsAllowedEmployer("Microsoft");

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsAllowedEmployer_WithAllowedEmployerGoogle_ReturnsTrue()
    {
        // Act
        var result = _checker.IsAllowedEmployer("Google");

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsAllowedEmployer_WithCaseInsensitivePluralsight_ReturnsTrue()
    {
        // Act
        var result = _checker.IsAllowedEmployer("pluralsight");

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsAllowedEmployer_WithCaseInsensitiveMicrosoft_ReturnsTrue()
    {
        // Act
        var result = _checker.IsAllowedEmployer("microsoft");

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsAllowedEmployer_WithCaseInsensitiveGoogle_ReturnsTrue()
    {
        // Act
        var result = _checker.IsAllowedEmployer("google");

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsAllowedEmployer_WithUpperCasePluralsight_ReturnsTrue()
    {
        // Act
        var result = _checker.IsAllowedEmployer("PLURALSIGHT");

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsAllowedEmployer_WithUpperCaseMicrosoft_ReturnsTrue()
    {
        // Act
        var result = _checker.IsAllowedEmployer("MICROSOFT");

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsAllowedEmployer_WithUpperCaseGoogle_ReturnsTrue()
    {
        // Act
        var result = _checker.IsAllowedEmployer("GOOGLE");

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsAllowedEmployer_WithNonAllowedEmployerApple_ReturnsFalse()
    {
        // Act
        var result = _checker.IsAllowedEmployer("Apple");

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsAllowedEmployer_WithNonAllowedEmployerAmazon_ReturnsFalse()
    {
        // Act
        var result = _checker.IsAllowedEmployer("Amazon");

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsAllowedEmployer_WithNonAllowedEmployerNetflix_ReturnsFalse()
    {
        // Act
        var result = _checker.IsAllowedEmployer("Netflix");

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsAllowedEmployer_WithEmptyEmployer_ReturnsFalse()
    {
        // Act
        var result = _checker.IsAllowedEmployer("");

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsAllowedEmployer_WithPartialMatchMicrosoftCorporation_ReturnsFalse()
    {
        // Act
        var result = _checker.IsAllowedEmployer("Microsoft Corporation");

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsAllowedEmployer_WithPartialMatchGoogleInc_ReturnsFalse()
    {
        // Act
        var result = _checker.IsAllowedEmployer("Google Inc");

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsAllowedEmployer_WithPartialMatchPluralsightLLC_ReturnsFalse()
    {
        // Act
        var result = _checker.IsAllowedEmployer("Pluralsight LLC");

        // Assert
        result.Should().BeFalse();
    }
}
