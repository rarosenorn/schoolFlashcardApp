using MediatR;
using flashcardApp.Shared.Features;
using System.Net.Http.Json;

public class EditDeckHandler : IRequestHandler<EditDeckRequest, EditDeckRequest.Response>
{
    private readonly HttpClient _httpClient;

    public EditDeckHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<EditDeckRequest.Response> Handle(EditDeckRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PutAsJsonAsync(EditDeckRequest.RouteTemplate, request, cancellationToken);

        if(response.IsSuccessStatusCode)
        {
            return new EditDeckRequest.Response(true);
        }
        else
        {
            return new EditDeckRequest.Response(false);
        }
    }
}