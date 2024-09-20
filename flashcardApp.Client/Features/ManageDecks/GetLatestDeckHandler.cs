using MediatR;
using flashcardApp.Shared.Features;
using System.Net.Http.Json;

namespace flashcardApp.Client.Features.ManageDecks;

public class GetLatestDeckHandler : IRequestHandler<GetLatestDeckRequest, GetLatestDeckRequest.Response>
{
    private readonly HttpClient _httpClient;

    public GetLatestDeckHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GetLatestDeckRequest.Response> Handle(GetLatestDeckRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<GetLatestDeckRequest.Response>(GetLatestDeckRequest.RouteTemplate);
        }
        catch (HttpRequestException)
        {
            return default;
        }
    }
}