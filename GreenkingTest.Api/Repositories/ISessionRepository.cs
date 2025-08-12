using GreenKingRefactoring.Speaker.Models;

namespace GreenKingRefactoring.Speaker.Repositories;

public interface ISessionRepository
{
    Task<IEnumerable<Session>> GetSessions(); // <>
}