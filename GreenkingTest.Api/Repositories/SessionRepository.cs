namespace GreenkingTest.Api.Repositories;
using Data.Models;


public class SessionRepository : ISessionRepository
{
    public async Task<IEnumerable<Session>> GetSessions()
    {
        return [];
    }
}