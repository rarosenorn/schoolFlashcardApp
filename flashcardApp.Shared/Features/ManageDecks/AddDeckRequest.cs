using MediatR;
using FluentValidation;
namespace flashcardApp.Shared.Features;

public record AddDeckRequest(DeckDto Deck) : IRequest<AddDeckRequest.Response>
{
    public const string RouteTemplate = "/api/decks";
    public record Response(int DeckId);
}

public class AddDeckRequestValidator : AbstractValidator<AddDeckRequest>
{
    public AddDeckRequestValidator()
    {
        RuleFor(x => x.Deck).SetValidator(new DeckValidator());
    }
}