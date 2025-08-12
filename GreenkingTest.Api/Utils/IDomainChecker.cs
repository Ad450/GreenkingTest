namespace GreenkingTest.Api.Utils;

public interface IDomainChecker
{
    public bool IsAllowedDomain(string email);
}