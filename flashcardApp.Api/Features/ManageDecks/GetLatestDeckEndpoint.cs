using Ardalis.ApiEndpoints;
using flashcardApp.Shared.Features;
using flashcardApp.Api.Persistence;
using flashcardApp.Api.Persistence.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace flashcardApp.Api.Features.ManageDecks;

public class GetLatestDeckEndpoint : BaseAsyncEndpoint.WithoutRequest.WithResponse<GetLatestDeckRequest.Response>
{
    private readonly DatabaseContext _dbContext;

    public GetLatestDeckEndpoint(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet(GetLatestDeckRequest.RouteTemplate)]
    public override async Task<ActionResult<GetLatestDeckRequest.Response>> HandleAsync(CancellationToken cancellationToken = default!)
    {

        var latestDeck = await _dbContext.Decks.OrderBy(x => x.Id).LastAsync();
        if(latestDeck == null)
        {
            return BadRequest("none found");
        }
        GetLatestDeckRequest.Deck returnDeck = new(latestDeck.Id, 
                                                   latestDeck.Name, 
                                                   latestDeck.Topic, 
                                                   latestDeck.NumberOfCards, 
                                                   latestDeck.Stars);

        var response = new GetLatestDeckRequest.Response(returnDeck);

        return Ok(response);
    }
}
