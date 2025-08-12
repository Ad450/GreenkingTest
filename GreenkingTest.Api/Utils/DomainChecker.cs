namespace GreenKingRefactoring.Speaker.Utils;

public class DomainChecker : IDomainChecker
{
    private readonly List<string> Names = 
    [
        "aol.com",
        "prodigy.com",
        "compuserve.com"
    ];
    
    public bool IsAllowedDomain(string email)
    {
        if (string.IsNullOrEmpty(email) || !email.Contains('@'))
            return false; 
        
        var domain = email.Split("@")[1];
        return !Names.Contains(domain);
    }
}