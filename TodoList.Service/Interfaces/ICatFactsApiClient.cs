namespace Service.Interfaces;

using Domain.Dtos;


public interface ICatFactsApiClient
{
    Task<CatFactsDto> GetCatFact();
        
}