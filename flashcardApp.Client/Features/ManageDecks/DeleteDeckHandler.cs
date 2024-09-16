using MediatR;
using flashcardApp.Shared.Features;
using System.Net.Http.Json;

public class DeleteDeckHandler : IRequestHandler<DeleteDeckRequest, DeleteDeckRequest.Response>
{
    private readonly HttpClient _httpClient;

    public DeleteDeckHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<DeleteDeckRequest.Response> Handle(DeleteDeckRequest request, CancellationToken cancellationToken)
    {
        var requestUri = $"{DeleteDeckRequest.RouteTemplate}/{request.DeckId}";

        var response = await _httpClient.DeleteAsync(requestUri, cancellationToken);

        if(response.IsSuccessStatusCode)
        {
            return new DeleteDeckRequest.Response(true);
        }
        else
        {
            return new DeleteDeckRequest.Response(false);
        }
    }
}