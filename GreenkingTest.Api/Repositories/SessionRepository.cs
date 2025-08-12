using GreenKingRefactoring.Speaker.Models;

namespace GreenKingRefactoring.Speaker.Repositories;

public class SessionRepository : ISessionRepository
{
    public async Task<IEnumerable<Session>> GetSessions()
    {
        return [];
    }
}