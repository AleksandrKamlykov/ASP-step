using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
namespace WebApplication1.Services;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private const string ApiKey = "71cc1e3e06ac488e8d7100543243108"; // Replace with your actual API key
    private const string BaseUrl = "http://api.weatherapi.com/v1/current.json";

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<object> GetWeatherDataAsync(string city)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}?key={ApiKey}&q={city}");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<object>(json);
        }
        return null;
    }
}