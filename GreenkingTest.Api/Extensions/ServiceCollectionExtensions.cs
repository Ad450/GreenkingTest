using GreenkingTest.Api.Repositories;
using GreenkingTest.Api.Services;
using GreenkingTest.Api.Utils;
using FluentValidation;
using GreenkingTest.Api.DataTransferObjects;
using GreenkingTest.Api.Validators;

namespace GreenkingTest.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISpeakerRegistrationService, SpeakerRegistrationService>();
        services.AddScoped<IDomainChecker, DomainChecker>();
        services.AddScoped<IEmployerChecker, EmployerChecker>();
        services.AddScoped<ISessionTopicChecker, SessionTopicChecker>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<ISpeakerRepository, SpeakerRepository>();
        services.AddScoped<IValidator<SpeakerDto>, SpeakerValidator>();
        
        return services;
    }

}