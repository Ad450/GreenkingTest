namespace GreenKingRefactoring.Speaker.Utils;
using Models;



public class SessionTopicChecker  : ISessionTopicChecker
{
    private readonly IList<string> topics = 
    [
        "Cobol", "Punch Cards", "Commodore", "VBScript"
    ];
    
    public bool IsAllowedTopic(IList<Session> sessions )
    {
     
        if (!sessions.Any()) return false;
        
        
        foreach (var session in sessions)
        {
            session.IsApproved = true;
            
            foreach (var topic in topics)
            {
                
                var isTopicInSession = session.Title.Contains(topic, StringComparison.OrdinalIgnoreCase) 
                                       || session.Description.Contains(topic, StringComparison.OrdinalIgnoreCase);

                if (isTopicInSession)
                {
                    session.IsApproved = false;
                    break;
                }
                
            }
            
        }

        return sessions.Any(s => s.IsApproved);
    }
}