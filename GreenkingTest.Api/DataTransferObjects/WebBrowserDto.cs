using GreenKingRefactoring.Speaker.Data.Enums;

namespace GreenKingRefactoring.Speaker.DataTransferObjects;

public class WebBrowserDto
{
    public BrowserName Name { get; set; }
    public int MajorVersion { get; set; }
}