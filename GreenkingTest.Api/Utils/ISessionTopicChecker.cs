using GreenKingRefactoring.Speaker.Models;

namespace GreenKingRefactoring.Speaker.Utils;

public interface ISessionTopicChecker
{
    public bool IsAllowedTopic(IList<Session> sessions);
}