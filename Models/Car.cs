using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public uint Price { get; set; }
        override public string ToString()
        {
            return $"Brand: {Brand},Model: {Model} Year: {Year}, Price: {Price}";
        }
    }
}
