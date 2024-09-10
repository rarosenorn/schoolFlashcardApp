using Microsoft.EntityFrameworkCore;
using flashcardApp.Api.Persistence.Models;
namespace flashcardApp.Api.Persistence;

public class DatabaseContext : DbContext 
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Deck> Decks {get; set;}
    public DbSet<Flashcard> Flashcards {get; set;}
}