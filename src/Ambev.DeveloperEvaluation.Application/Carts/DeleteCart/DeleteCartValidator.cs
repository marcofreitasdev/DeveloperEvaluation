using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

/// <summary>
/// Validator for DeleteCartCommand that defines validation rules for cart deletion.
/// </summary>
public class DeleteCartValidator : AbstractValidator<DeleteCartCommand>
{
    /// <summary>
    /// Initializes a new instance of the DeleteCartValidator with defined validation rules.
    /// </summary>
    public DeleteCartValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Cart Id is required.")
            .Must(BeAValidGuid)
            .WithMessage("Cart Id must be a valid GUID.");
    }

    /// <summary>
    /// Validates if the provided Id is a valid GUID and not empty.
    /// </summary>
    /// <param name="id">The Id to validate.</param>
    /// <returns>True if the Id is a valid GUID and not empty; otherwise, false.</returns>
    private bool BeAValidGuid(Guid id)
    {
        return id != Guid.Empty;
    }
}