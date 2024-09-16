using MediatR;
using flashcardApp.Shared.Features;
using System.Net.Http.Json;

public class GetDeckHandler : IRequestHandler<GetDeckRequest, GetDeckRequest.Response?>
{
    private readonly HttpClient _httpClient;

    public GetDeckHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GetDeckRequest.Response> Handle(GetDeckRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<GetDeckRequest.Response>(GetDeckRequest.RouteTemplate.Replace("{deckId}", request.DeckId.ToString()));
        }
        catch (HttpRequestException)
        {
            return default;
        }
    }
}