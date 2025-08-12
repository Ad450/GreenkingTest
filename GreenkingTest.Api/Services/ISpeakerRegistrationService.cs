namespace GreenkingTest.Api.Services;
using DataTransferObjects;


public interface ISpeakerRegistrationService
{
    public Task<RegistrationResponse<string>> RegisterSpeaker(SpeakerDto speaker);
    
}