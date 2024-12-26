using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

/// <summary>
/// Validator for DeleteProductCommand that defines validation rules for product deletion.
/// </summary>
public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the DeleteProductValidator with defined validation rules.
    /// </summary>
    public DeleteProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product Id is required.")
            .Must(BeAValidGuid)
            .WithMessage("Product Id must be a valid GUID.");
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