using Ardalis.ApiEndpoints;
using flashcardApp.Shared.Features;
using flashcardApp.Api.Persistence;
using flashcardApp.Api.Persistence.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace flashcardApp.Api.Features.ManageDecks;

public class GetDecksEndpoint : BaseAsyncEndpoint.WithoutRequest.WithResponse<GetDecksRequest.Response>
{
    private readonly DatabaseContext _dbContext;

    public GetDecksEndpoint(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet(GetDecksRequest.RouteTemplate)]
    public override async Task<ActionResult<GetDecksRequest.Response>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var decks = await _dbContext.Decks.Include(x => x.Flashcards)
                                          .ToListAsync(cancellationToken);
                                          
        var response = new GetDecksRequest.Response(decks.Select(deck => new GetDecksRequest.Deck
            (
                deck.Id,
                deck.Name,
                deck.Topic,
                deck.NumberOfCards,
                deck.Stars
            )));

        return Ok(response);
    }
}



