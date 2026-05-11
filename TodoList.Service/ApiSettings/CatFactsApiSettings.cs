namespace Service.ApiSettings;

public class CatFactsApiSettings
{
    public string BaseUrl { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public int TimeoutSeconds { get; set; } = 30;
    public int CacheDurationMinutes { get; set; } = 120;
}