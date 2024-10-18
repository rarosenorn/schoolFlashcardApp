using flashcardApp.Shared.Features;
using flashcardApp.Api.Persistence;
using flashcardApp.Api.Persistence.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace flashcardApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DeckController : ControllerBase
{
    private readonly DatabaseContext _dbContext;
    public DeckController(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<DeckDto>>> GetAsync()
    {
        try
        {
            var decks = await _dbContext.Decks.ToListAsync();

            if (decks != null)
            {
                List<DeckDto> deckDtos = decks.Select(x => new DeckDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Topic = x.Topic,
                    NumberOfCards = x.NumberOfCards,
                    Stars = x.Stars
                }).ToList();

                return Ok(deckDtos);
            }
            return NotFound();
        }
        catch
        {
            return StatusCode(500);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DeckDto>> GetByIdAsync(int id)
    {
        try
        {
            var deck = await _dbContext.Decks.Include(x => x.Flashcards).SingleOrDefaultAsync(x => x.Id == id);

            if (deck != null)
            {
                DeckDto deckDto = new()
                {
                    Id = deck.Id,
                    Name = deck.Name,
                    Topic = deck.Topic,
                    NumberOfCards = deck.NumberOfCards,
                    Stars = deck.Stars,
                    Flashcards = deck.Flashcards.Select(x => new FlashcardDto
                    {
                        Id = x.Id,
                        Frontside = x.Frontside,
                        Backside = x.Backside,
                        SubTopic = x.SubTopic,
                        DeckId = x.DeckId
                    }).ToList()
                };

                return Ok(deckDto);
            }
            return NotFound();
        }
        catch
        {
            return StatusCode(500);
        }
    }

    [HttpGet("latest")]
    public async Task<ActionResult<DeckDto>> GetLatest()
    {
        try
        {
            var latestDeck = await _dbContext.Decks.OrderBy(x => x.Id).LastAsync();

            if (latestDeck != null)
            {
                DeckDto latestdeckDto = new()
                {
                    Id = latestDeck.Id,
                    Name = latestDeck.Name,
                    Topic = latestDeck.Topic,
                    NumberOfCards = latestDeck.NumberOfCards,
                    Stars = latestDeck.Stars
                };

                return Ok(latestdeckDto);
            }
            return NotFound();
        }
        catch
        {
            return StatusCode(400);
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(DeckDto deckDto)
    {
        try
        {
            var deck = new Deck()
            {
                Name = deckDto.Name
            };

            _dbContext.Decks.Add(deck);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        catch
        {
            return StatusCode(400);
        }
    }

    [HttpPut]
    public async Task<ActionResult> EditAsync(DeckDto deckDto)
    {
        try
        {
            var deck = await _dbContext.Decks.Include(x => x.Flashcards).SingleOrDefaultAsync(x => x.Id == deckDto.Id);

            if (deck == null)
            {
                return NotFound();
            }

            deck.Name = deckDto.Name;
            deck.Topic = deckDto.Topic;
            deck.NumberOfCards = deckDto.NumberOfCards;
            deck.Stars = deckDto.Stars;

            List<Flashcard> flashcards = deckDto.Flashcards.Select(x => new Flashcard
            {
                Frontside = x.Frontside,
                Backside = x.Backside,
                SubTopic = x.SubTopic,
            }).ToList();

            deck.Flashcards = flashcards;
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        catch
        {
            return StatusCode(500);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        try
        {
            var deck = await _dbContext.Decks.FindAsync(id);
            if (deck != null)
            {
                _dbContext.Decks.Remove(deck);
                await _dbContext.SaveChangesAsync();
                return NoContent();
            }
            return NotFound();
        }
        catch
        {
            return StatusCode(500);
        }
    }
}