using FluentAssertions;
using GreenkingTest.Api.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace GreenkingTest.Test.Extensions;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddServices_ShouldRegisterAllRequiredServices()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new Mock<IConfiguration>();

        // Act
        services.AddServices(configuration.Object);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        
        // Check if services are registered
        serviceProvider.GetService<GreenkingTest.Api.Services.ISpeakerRegistrationService>().Should().NotBeNull();
        serviceProvider.GetService<GreenkingTest.Api.Utils.IDomainChecker>().Should().NotBeNull();
        serviceProvider.GetService<GreenkingTest.Api.Utils.IEmployerChecker>().Should().NotBeNull();
        serviceProvider.GetService<GreenkingTest.Api.Utils.ISessionTopicChecker>().Should().NotBeNull();
        serviceProvider.GetService<GreenkingTest.Api.Repositories.ISessionRepository>().Should().NotBeNull();
        serviceProvider.GetService<GreenkingTest.Api.Repositories.ISpeakerRepository>().Should().NotBeNull();
        serviceProvider.GetService<FluentValidation.IValidator<GreenkingTest.Api.DataTransferObjects.SpeakerDto>>().Should().NotBeNull();
    }

    [Fact]
    public void AddServices_ShouldRegisterServicesAsScoped()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new Mock<IConfiguration>();

        // Act
        services.AddServices(configuration.Object);

        // Assert
        var serviceDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(GreenkingTest.Api.Services.ISpeakerRegistrationService));
        serviceDescriptor.Should().NotBeNull();
        serviceDescriptor!.Lifetime.Should().Be(ServiceLifetime.Scoped);
    }

    [Fact]
    public void AddServices_ShouldReturnServiceCollection()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new Mock<IConfiguration>();

        // Act
        var result = services.AddServices(configuration.Object);

        // Assert
        result.Should().BeSameAs(services);
    }
}
