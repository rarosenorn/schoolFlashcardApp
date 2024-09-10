using FluentValidation;

namespace flashcardApp.Shared.Features;
public class DeckDto 
{
    public int Id {get; set;}
    public string? Name {get; set;} = "";
    public string? Topic {get; set;} = "";
    public int NumberOfCards {get; set;} = 0;
    public decimal? Stars {get; set;}

    public List<FlashcardDto> Flashcards {get; set;} = new List<FlashcardDto>();
}

public class DeckValidator : AbstractValidator<DeckDto>
{
    public DeckValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Pwease enter a name");
        RuleFor(x => x.NumberOfCards).GreaterThanOrEqualTo(0).WithMessage("Cant have negative flashcards!!");

        RuleForEach(x => x.Flashcards).SetValidator(new FlashcardValidator());
    }
}