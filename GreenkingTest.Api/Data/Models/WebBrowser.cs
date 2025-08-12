namespace GreenkingTest.Api.Data.Models;
using Enums;


public class WebBrowser
{
    public int Id { get; set; }
    public BrowserName Name { get; set; }
    public int MajorVersion { get; set; }
}