using GreenKingRefactoring.Speaker.Data.Enums;

namespace GreenKingRefactoring.Speaker.Models;

public class WebBrowser
{
    public int Id { get; set; }
    public BrowserName Name { get; set; }
    public int MajorVersion { get; set; }
}