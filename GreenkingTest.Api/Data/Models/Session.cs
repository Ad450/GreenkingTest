namespace GreenkingTest.Api.Data.Models;
using DataTransferObjects;




public class Session
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsApproved { get; set; }

    public static Session CreateFromSessionDto(SessionDto session) => new Session()
    {
        Title = session.Title,
        Description = session.Description,
        IsApproved = false
    };
}