namespace GreenkingTest.Api.Repositories;

using GreenkingTest.Api.Data.Models;

public interface ISessionRepository
{
    Task<IEnumerable<Session>> GetSessions(); // <>
}