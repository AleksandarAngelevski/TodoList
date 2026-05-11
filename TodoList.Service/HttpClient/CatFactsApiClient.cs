using System.Net.Http.Json;
using Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Service.ApiSettings;
using Service.Interfaces;

namespace Repository.HttpClient;

public class CatFactsApiClient : ICatFactsApiClient
{
    private readonly System.Net.Http.HttpClient _http;
    private readonly CatFactsApiSettings _settings;
    private readonly ILogger<CatFactsApiClient> _logger;

    public CatFactsApiClient(System.Net.Http.HttpClient http, IOptions<CatFactsApiSettings> options,
        ILogger<CatFactsApiClient> logger)
    {
        _http = http;
        _settings = options.Value;
        _logger = logger;
    }

    public async Task<CatFactsDto> GetCatFact()
    {
        var response = await _http.GetAsync("/fact");
        response.EnsureSuccessStatusCode();

        var catFact = await response.Content.ReadFromJsonAsync<CatFactsDto>();


        return catFact;
    }
}