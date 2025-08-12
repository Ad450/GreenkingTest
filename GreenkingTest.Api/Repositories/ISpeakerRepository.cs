namespace GreenKingRefactoring.Speaker.Repositories;
using Models;

public interface ISpeakerRepository
{
    public Task<string> SaveSpeaker(Speaker speaker);
}