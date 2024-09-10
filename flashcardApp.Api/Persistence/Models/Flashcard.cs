namespace flashcardApp.Api.Persistence.Models;

public class Flashcard
{
    public int Id {get; set;}
    public string? Frontside {get; set;}
    public string? Backside {get; set;}
    public string? SubTopic {get; set;}
    public int DeckId {get; set;}
}