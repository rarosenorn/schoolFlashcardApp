using Ardalis.ApiEndpoints;
using flashcardApp.Shared.Features;
using flashcardApp.Api.Persistence;
using flashcardApp.Api.Persistence.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace flashcardApp.Api.Features.ManageDeck;

public class GetDeckEndpoint : BaseAsyncEndpoint.WithRequest<int>.WithResponse<GetDeckRequest.Response>
{
    private readonly DatabaseContext _dbContext;

    public GetDeckEndpoint(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet(GetDeckRequest.RouteTemplate)]
    public override async Task<ActionResult<GetDeckRequest.Response>> HandleAsync(int deckId, CancellationToken cancellationToken = default)
    {
        var deck = await _dbContext.Decks.Include(x => x.Flashcards)
                                         .SingleOrDefaultAsync(x => x.Id == deckId, cancellationToken: cancellationToken);

        if (deck is null)
        {
            return BadRequest("ffs");
        }

        IEnumerable<GetDeckRequest.Flashcard> flashcards = default;

        if (deck.Flashcards != null)
        {
            flashcards = deck.Flashcards.Select(x => new GetDeckRequest.Flashcard
            (
                x.Id,
                x.Frontside,
                x.Backside,
                x.SubTopic,
                x.DeckId
            ));
        }


        var returnDeck = new GetDeckRequest.Deck
            (
                deck.Id,
                deck.Name,
                deck.Topic,
                deck.NumberOfCards,
                deck.Stars,
                flashcards
            );

        var response = new GetDeckRequest.Response(returnDeck);

        return Ok(response);
    }
}

