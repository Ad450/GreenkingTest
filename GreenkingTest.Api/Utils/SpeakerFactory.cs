using GreenkingTest.Api.Data.Models;
using GreenkingTest.Api.DataTransferObjects;

namespace GreenkingTest.Api.Utils;

public class SpeakerFactory(IRegistrationFeeHelper registrationFeeHelper) : ISpeakerFactory
{
    public Speaker CreateSpeakerFromDto(SpeakerDto speaker)
    {
        return new Speaker
        {
            FirstName = speaker.FirstName,
            LastName = speaker.LastName,
            Blog = Blog.CreateFromDto(speaker.Blog),
            Certifications = speaker.Certifications.Select(Certification.CreateFromDto).ToList(),
            Experience = speaker.Experience,
            RegistrationFee = registrationFeeHelper.GetRegistrationFee(speaker.Experience)
        };
    }
}