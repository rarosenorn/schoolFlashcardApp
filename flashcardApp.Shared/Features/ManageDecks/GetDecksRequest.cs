using MediatR;

namespace flashcardApp.Shared.Features;

public record GetDecksRequest : IRequest<GetDecksRequest.Response>
{
    public const string RouteTemplate = "/api/getdecks";

    public record Deck(int Id, string Name, string Topic, int NumberOfCards, decimal? Stars);
    public record Response(IEnumerable<Deck> Decks);
}