namespace GreenkingTest.Api.Utils;

public class EmployerChecker : IEmployerChecker
{
    private readonly List<string> Employers = 
    [
        "Pluralsight", "Microsoft", "Google"
    ];
    
    public bool IsAllowedEmployer(string employer)
    {
        return Employers.Any(emp => emp.ToLower().Equals(employer.ToLower()));
    }
}