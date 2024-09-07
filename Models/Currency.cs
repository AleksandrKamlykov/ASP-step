namespace WebApplication1.Models;

public class Currency
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Text { get; set; }
    public string Cc { get; set; }
    public float Rate { get; set; }
}