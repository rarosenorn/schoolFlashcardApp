using MediatR;
using flashcardApp.Shared.Features;
using System.Net.Http.Json;

public class GetDecksHandler : IRequestHandler<GetDecksRequest, GetDecksRequest.Response>
{
    private readonly HttpClient _httpClient;

    public GetDecksHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GetDecksRequest.Response> Handle(GetDecksRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<GetDecksRequest.Response>(GetDecksRequest.RouteTemplate);
        }
        catch (HttpRequestException)
        {
            return default;
        }
    }
}