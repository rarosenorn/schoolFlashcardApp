using MediatR;
using flashcardApp.Shared.Features;
using System.Net.Http.Json;
namespace flashcardApp.Client.Features.ManageDecks;

public class AddDeckHandler : IRequestHandler<AddDeckRequest, AddDeckRequest.Response>
{
    private readonly HttpClient _httpClient;

    public AddDeckHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<AddDeckRequest.Response> Handle(AddDeckRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync(AddDeckRequest.RouteTemplate, request, cancellationToken);

        if(response.IsSuccessStatusCode)
        {
            var deckId = await response.Content.ReadFromJsonAsync<int>(cancellationToken:cancellationToken);
            return new AddDeckRequest.Response(deckId);
        }
        else
        {
            return new AddDeckRequest.Response(-1);
        }
    }
}