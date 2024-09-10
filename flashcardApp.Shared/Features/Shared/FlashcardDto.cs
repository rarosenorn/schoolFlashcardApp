using FluentValidation;

namespace flashcardApp.Shared.Features;
public class FlashcardDto   
{
    public int Id { get; set; }
    public string? Frontside { get; set; } = "";
    public string? Backside { get; set; } = "";
    public string? SubTopic { get; set; } = "";
    public int DeckId { get; set; }
}

public class FlashcardValidator : AbstractValidator<FlashcardDto>
{
    public FlashcardValidator()
    {
        RuleFor(x => x.Frontside).NotEmpty().WithMessage("Cant be empty!<:)");
    }
}