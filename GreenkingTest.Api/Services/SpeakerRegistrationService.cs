namespace GreenkingTest.Api.Services;
using FluentValidation;
using Data.Enums;
using Data.Models;
using DataTransferObjects;
using Repositories;
using Utils;


public class SpeakerRegistrationService(
    IValidator<SpeakerDto> speakerValidator, 
    IEmployerChecker employerChecker,
    IDomainChecker domainChecker,
    ISessionTopicChecker sessionTopicChecker,
    ISpeakerRepository speakerRepository,
    ISpeakerFactory speakerFactory
    ) : ISpeakerRegistrationService

{

    public async Task<RegistrationResponse<string>> RegisterSpeaker(SpeakerDto speaker)
    {
        var modelState = await ValidateSpeakerAsync(speaker);
        if (!modelState.IsValid)
            return RegistrationResponse<string>.ErrorResponse(modelState.Errors.First().ErrorMessage);

        if (!MeetsMinimumStandards(speaker))
            return RegistrationResponse<string>.ErrorResponse("Speaker does not meet standards");

        if (!IsSessionApproved(speaker))
            return RegistrationResponse<string>.ErrorResponse("Session not Approved");

        var approvedSpeaker = speakerFactory.CreateSpeakerFromDto(speaker);
        var speakerId = await speakerRepository.SaveSpeaker(approvedSpeaker);

        return RegistrationResponse<string>.SuccessResponse(speakerId);
    }
    
    
    private async Task<FluentValidation.Results.ValidationResult> ValidateSpeakerAsync(SpeakerDto speaker)
    {
        return await speakerValidator.ValidateAsync(speaker);
    }
    
    
    private bool MeetsMinimumStandards(SpeakerDto speaker)
    {
        const int minimumExperience = 10;
        const int minimumCertificationCount = 3;
        const int minimumBrowserMajorVersion = 9;

        var hasRequiredExperience = speaker.Experience > minimumExperience;
        var hasBlog = speaker.Blog != null;
        var hasEnoughCertifications = speaker.Certifications.Count() > minimumCertificationCount;
        var isFromBigTech = employerChecker.IsAllowedEmployer(speaker.Employer);

        var meetsStandard = hasRequiredExperience || hasBlog || hasEnoughCertifications || isFromBigTech;

        if (meetsStandard) return meetsStandard;

        var isAllowedDomain = domainChecker.IsAllowedDomain(speaker.Email);
        var isAllowedBrowser = !speaker.Browser.Name.Equals(BrowserName.InternetExplorer) &&
                                speaker.Browser.MajorVersion >= minimumBrowserMajorVersion;

        return isAllowedDomain && isAllowedBrowser;
    }
    

    
    private bool IsSessionApproved(SpeakerDto speaker)
    {
        var sessions = speaker.Sessions.Select(Session.CreateFromSessionDto).ToList();
        return sessionTopicChecker.IsAllowedTopic(sessions);
    }
    

    

}

