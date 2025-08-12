namespace GreenkingTest.Api.Data.Models;


public class Speaker
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Employer { get; set; }
    public int? Experience { get; set; }
    public Blog? Blog { get; set; }
    public List<Certification> Certifications { get; set; }
    public int RegistrationFee { get; set; }
    public List<Session> Sessions { get; set; }
    
}