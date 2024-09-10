using MediatR;

namespace flashcardApp.Shared.Features;

public record GetDeckRequest(int DeckId) : IRequest<GetDeckRequest.Response>
{
    public const string RouteTemplate = "/api/getdeck/{deckId}";

    public record Response(Deck Deck);
    public record Deck(int Id, string Name, string? Topic, int NumberOfCards, decimal? Stars, IEnumerable<Flashcard>? Flashcards);
    public record Flashcard(int Id, string? Frontside, string? Backside, string? SubTopic, int DeckId);
}

