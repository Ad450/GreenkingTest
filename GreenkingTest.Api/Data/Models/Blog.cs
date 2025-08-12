namespace GreenkingTest.Api.Data.Models;
using DataTransferObjects;




public class Blog
{
    public int Id { get; set; }
    public string Url { get; set; }

    public static Blog CreateFromDto(BlogDto dto)
    {
        return new Blog()
        {
            Url = dto.Url
        };
    }
}