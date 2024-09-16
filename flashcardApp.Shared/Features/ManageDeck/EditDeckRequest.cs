using FluentValidation;
using MediatR;

namespace flashcardApp.Shared.Features;

public record EditDeckRequest(DeckDto Deck) : IRequest<EditDeckRequest.Response>
{
    public const string RouteTemplate = "/api/editdeck";

    public record Response(bool IsSuccess);
}


