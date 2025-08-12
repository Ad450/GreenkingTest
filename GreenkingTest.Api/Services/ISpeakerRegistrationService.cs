using GreenKingRefactoring.Speaker.DataTransferObjects;

namespace GreenKingRefactoring.Speaker.Services;

public interface ISpeakerRegistrationService
{
    public Task<RegistrationResponse<string>> RegisterSpeaker(SpeakerDto speaker);
    
}