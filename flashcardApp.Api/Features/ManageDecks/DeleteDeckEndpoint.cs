using Ardalis.ApiEndpoints;
using flashcardApp.Shared.Features;
using flashcardApp.Api.Persistence;
using flashcardApp.Api.Persistence.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace flashcardApp.Api.Features.ManageDecks;

public class DeleteDeckEndpoint : BaseAsyncEndpoint.WithRequest<int>.WithResponse<bool> 
{
    private readonly DatabaseContext _dbContext;

    public DeleteDeckEndpoint(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpDelete($"{DeleteDeckRequest.RouteTemplate}/{{deckId}}")]
    public override async Task<ActionResult<bool>> HandleAsync([FromRoute] int deckId, CancellationToken cancellationToken)
    {
        var deck = await _dbContext.Decks.Include(x => x.Flashcards).SingleOrDefaultAsync(x => x.Id == deckId);

        if(deck == null)
        {
            return BadRequest("Deck not found.");
        }
        foreach(var f in deck.Flashcards)
        {
            _dbContext.Flashcards.Remove(f);
        }
        _dbContext.Decks.Remove(deck);

        await _dbContext.SaveChangesAsync();

        return Ok(true);
    }
}
