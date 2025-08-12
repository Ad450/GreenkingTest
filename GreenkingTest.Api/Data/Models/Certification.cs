using GreenKingRefactoring.Speaker.DataTransferObjects;

namespace GreenKingRefactoring.Speaker.Models;

public class Certification
{
    public int Id { get; set; }
    public string Name { get; set; }

    public static Certification CreateFromDto(CertificationDto dto)
    {
        return new Certification()
        {
            Name = dto.Name
        };
    }
}