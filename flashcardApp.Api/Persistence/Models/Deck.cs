namespace flashcardApp.Models;
public class Deck
{
    public int Id {get; set;}
    public string? Name {get; set;}
    public string? Topic {get; set;}
    public int NumberOfCards {get; set;} = 0;
    public decimal? Stars {get; set;}

    public List<Flashcard> Flashcards {get; set;} = default!;
}