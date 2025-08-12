namespace GreenkingTest.Api.Utils;
using Data.Models;
using DataTransferObjects;


public interface ISpeakerFactory
{
    Speaker CreateSpeakerFromDto(SpeakerDto speaker);
}