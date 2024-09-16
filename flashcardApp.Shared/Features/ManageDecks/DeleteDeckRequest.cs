using MediatR;

namespace flashcardApp.Shared.Features;

public record DeleteDeckRequest(int DeckId) : IRequest<DeleteDeckRequest.Response>
{
    public const string RouteTemplate = "/api/deletedeck";

    public record Response(bool IsSuccess);
}
