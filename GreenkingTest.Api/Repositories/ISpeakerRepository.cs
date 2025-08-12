namespace GreenkingTest.Api.Repositories;
using Data.Models;


public interface ISpeakerRepository
{
    public Task<string> SaveSpeaker(Speaker speaker);
}