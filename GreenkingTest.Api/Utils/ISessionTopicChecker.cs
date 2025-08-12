namespace GreenkingTest.Api.Utils;
using Data.Models;


public interface ISessionTopicChecker
{
    public bool IsAllowedTopic(IList<Session> sessions);
}