using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;

/// <summary>
/// Validator for DeleteCartRequest that defines validation rules for cart deletion.
/// </summary>
public class DeleteCartRequestValidator : AbstractValidator<DeleteCartRequest>
{
    /// <summary>
    /// Initializes a new instance of the DeleteCartRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must be a valid non-empty GUID
    /// </remarks>
    public DeleteCartRequestValidator()
    {
        RuleFor(request => request.Id)
            .NotEmpty().WithMessage("Cart Id is required.")
            .Must(BeAValidGuid).WithMessage("Cart Id must be a valid GUID.");
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