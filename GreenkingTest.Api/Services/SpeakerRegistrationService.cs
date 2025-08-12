using GreenKingRefactoring.Speaker.Repositories;

namespace GreenKingRefactoring.Speaker.Services;
using FluentValidation;
using Data.Enums;
using DataTransferObjects;
using Models;
using Utils;



public class SpeakerRegistrationService(
    IValidator<SpeakerDto> speakerValidator, 
    IEmployerChecker employerChecker,
    IDomainChecker domainChecker,
    ISessionTopicChecker sessionTopicChecker,
    ISpeakerRepository speakerRepository
    ) : ISpeakerRegistrationService

{
    
    public async Task<RegistrationResponse<string>> RegisterSpeaker(SpeakerDto speaker)
    {
       var modelState =  await speakerValidator.ValidateAsync(speaker);
       if (!modelState.IsValid)
       {
           return RegistrationResponse<string>.ErrorResponse(modelState.Errors.First().ErrorMessage);
       }

       var speakerMeetsExperience = speaker.Experience is > 10;
       var speakerHasBlog = speaker.Blog != null;
       var speakerHasMorethanThreeCertifications = speaker.Certifications.Count() > 3;
       var speakerIsFromBigTech = employerChecker.IsAllowedEmployer(speaker.Employer);
       
       var speakerMeetsStandards = speakerMeetsExperience || 
                                   speakerHasBlog || 
                                   speakerHasMorethanThreeCertifications || 
                                   speakerIsFromBigTech;

       if (!speakerMeetsStandards)
       {
           var isAllowedDomain = domainChecker.IsAllowedDomain(speaker.Email);
           var isAllowedBrowser = !speaker.Browser.Name.Equals(BrowserName.InternetExplorer) &&
                                  (speaker.Browser.MajorVersion >= 9);
           speakerMeetsStandards = isAllowedDomain && isAllowedBrowser;
       }
       
       if(!speakerMeetsStandards) return RegistrationResponse<string>.ErrorResponse("Speaker does not meet standards");
       
       var sessions = speaker.Sessions.Select(Session.CreateFromSessionDto).ToList();
       var isSessionApproved = sessionTopicChecker.IsAllowedTopic(sessions);
       
       if(!isSessionApproved) return RegistrationResponse<string>.ErrorResponse("Session not Approved");

       var registrationFee = GetRegistrationFee(speaker.Experience);
       
       var approvedSpeaker  =  new Speaker()
       {
           FirstName = speaker.FirstName,
           LastName = speaker.LastName,
           Blog = Blog.CreateFromDto(speaker.Blog),
           Certifications = speaker.Certifications.Select(Certification.CreateFromDto).ToList(),
           Experience = speaker.Experience,
           RegistrationFee = registrationFee
       };
       
        var speakerId = await speakerRepository.SaveSpeaker(approvedSpeaker);
        return RegistrationResponse<string>.SuccessResponse(speakerId.ToString());
    }
    
    
    private int GetRegistrationFee(int? exp)
    {
        var registrationFee = exp switch
        {
            <= 1 => 500,
            >= 2 and <= 3 => 250,
            >= 4 and <= 5 => 100,
            >= 6 and <= 9 => 50,
            _ => 0
        };
        return registrationFee;
    }


}

