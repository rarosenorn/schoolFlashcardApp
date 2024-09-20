using MediatR;

namespace flashcardApp.Shared.Features;

public record GetLatestDeckRequest : IRequest<GetLatestDeckRequest.Response>
{
    public const string RouteTemplate = "/api/getlatestdeck";

    public record Response(Deck Deck);
    public record Deck(int Id, string Name, string Topic, int NumberOfCards, decimal? Stars);
}