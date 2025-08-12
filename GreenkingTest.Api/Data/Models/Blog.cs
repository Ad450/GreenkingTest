using GreenKingRefactoring.Speaker.DataTransferObjects;

namespace GreenKingRefactoring.Speaker.Models;

public class Blog
{
    int Id { get; set; }
    string Url { get; set; }

    public static Blog CreateFromDto(BlogDto dto)
    {
        return new Blog()
        {
            Url = dto.Url
        };
    }
}