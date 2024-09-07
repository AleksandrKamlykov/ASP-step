using System.Text;

namespace WebApplication1;

public class Page
{
    private StringBuilder Content  = new StringBuilder(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "html", "layout.html")));
    
    
    public void AddContent(string path)
    {
        Content.Replace("<!--content-->", File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "html", path)));
    }

    public void AddHeader(string path)
    {
        Content.Replace("<!--header-->", File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "html", path)));

    }
    public void AddScripts(string path)
    {
        Content.Replace("<!--scripts-->", File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "js", path)));
    }
    public string GetContent()
    {
        return Content.ToString();
    }
    
}