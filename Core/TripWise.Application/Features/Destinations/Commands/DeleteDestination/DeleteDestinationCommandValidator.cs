using FluentValidation;

namespace TripWise.Application.Features.Destinations.Commands.DeleteDestination;

public sealed class DeleteDestinationCommandValidator: AbstractValidator<DeleteDestinationCommand>
{
    public DeleteDestinationCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Destinasyon ID bilgisi boş olamaz.");
    }
}