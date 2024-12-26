using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;

/// <summary>
/// Validator for DeleteProductRequest that defines validation rules for product deletion.
/// </summary>
public class DeleteProductRequestValidator : AbstractValidator<DeleteProductRequest>
{
    /// <summary>
    /// Initializes a new instance of the DeleteProductRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must be a valid non-empty GUID
    /// </remarks>
    public DeleteProductRequestValidator()
    {
        RuleFor(request => request.Id)
            .NotEmpty().WithMessage("Product Id is required.")
            .Must(BeAValidGuid).WithMessage("Product Id must be a valid GUID.");
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