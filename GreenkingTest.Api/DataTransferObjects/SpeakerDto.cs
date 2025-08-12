namespace GreenkingTest.Api.DataTransferObjects;


public class SpeakerDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int? Experience { get; set; }
    public BlogDto? Blog { get; set; }
    public List<SessionDto> Sessions { get; set; }
    public List<CertificationDto> Certifications { get; set; }
    public string Employer { get; set; }
    public WebBrowserDto Browser { get; set; }
}