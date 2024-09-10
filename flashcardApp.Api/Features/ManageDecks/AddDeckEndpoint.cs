using Ardalis.ApiEndpoints;
using flashcardApp.Shared.Features;
using flashcardApp.Api.Persistence;
using flashcardApp.Api.Persistence.Models;
using Microsoft.AspNetCore.Mvc;

namespace flashcardApp.Api.Features.ManageDecks;

public class AddDeckEndpoint : BaseAsyncEndpoint.WithRequest<AddDeckRequest>.WithResponse<int>
{
    private readonly DatabaseContext _dbContext;

    public AddDeckEndpoint(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost(AddDeckRequest.RouteTemplate)]
    public override async Task<ActionResult<int>> HandleAsync(AddDeckRequest request, CancellationToken cancellationToken = default)
    {
        var deck = new Deck
        {
            Name = request.Deck.Name
        };

        await _dbContext.Decks.AddAsync(deck, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok(deck.Id);
    }
}