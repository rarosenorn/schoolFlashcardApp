using Ardalis.ApiEndpoints;
using flashcardApp.Shared.Features;
using flashcardApp.Api.Persistence;
using flashcardApp.Api.Persistence.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace flashcardApp.Api.Features.ManageDeck;

public class EditDeckEndpoint : BaseAsyncEndpoint.WithRequest<EditDeckRequest>.WithResponse<bool>
{
    private readonly DatabaseContext _dbContext;

    public EditDeckEndpoint(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPut(EditDeckRequest.RouteTemplate)]
    public override async Task<ActionResult<bool>> HandleAsync(EditDeckRequest request, CancellationToken cancellationToken = default)
    {
        var deck = await _dbContext.Decks.Include(x => x.Flashcards).SingleOrDefaultAsync(x => x.Id == request.Deck.Id, cancellationToken: cancellationToken);

        if(deck == null)
        {
            return BadRequest("Deck not found.");
        }

        deck.Name = request.Deck.Name;
        deck.NumberOfCards = request.Deck.NumberOfCards;
        deck.Stars = request.Deck.Stars;
        deck.Topic = request.Deck.Topic;
        deck.Flashcards = request.Deck.Flashcards.Select(f => new Flashcard 
        {
            Frontside = f.Frontside,
            Backside = f.Backside,
            SubTopic = f.SubTopic,
            DeckId = deck.Id
        }).ToList();

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok(true);
    }
}