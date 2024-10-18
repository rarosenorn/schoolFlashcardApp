using flashcardApp.Shared.Features;

public interface IDecksService
{
    Task<List<DeckDto>> GetDecksAsync();
    Task<DeckDto> GetDeckByIdAsync(int id);
    Task<DeckDto> GetLatestDeckAsync();
    Task DeleteDeckAsync(int id);
    Task<string> UpdateDeckAsync(DeckDto deck);
    Task CreateDeckAsync(DeckDto deck);
}