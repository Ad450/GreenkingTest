namespace GreenKingRefactoring.Speaker.Utils;

public interface IDomainChecker
{
    public bool IsAllowedDomain(string email);
}