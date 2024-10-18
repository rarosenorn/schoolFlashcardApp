using System.Net.Http.Json;
using flashcardApp.Shared.Features;

public class DecksService : IDecksService
{
    private readonly HttpClient _httpClient;
    public DecksService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateDeckAsync(DeckDto deck)
    {
        await _httpClient.PostAsJsonAsync("/deck", deck);
    }

    public async Task DeleteDeckAsync(int id)
    {
        await _httpClient.DeleteAsync($"/deck/{id}");
    }

    public async Task<DeckDto> GetDeckByIdAsync(int id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"/deck/{id}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<DeckDto>();
        }
        else
        {
            Console.WriteLine("no deck found");
            return null;
        }
    }

    public async Task<List<DeckDto>> GetDecksAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<DeckDto>>("/deck");
    }

    public async Task<DeckDto> GetLatestDeckAsync()
    {
        return await _httpClient.GetFromJsonAsync<DeckDto>("/deck/latest");
    }

    public async Task<string> UpdateDeckAsync(DeckDto deck)
    {
        var response = await _httpClient.PutAsJsonAsync("/deck", deck);
        if (response.IsSuccessStatusCode)
        {
            return "Successfully Saved";
        }
        else
        {
            return "somting wong";
        }
    }
}