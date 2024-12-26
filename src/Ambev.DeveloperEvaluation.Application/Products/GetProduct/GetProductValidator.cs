using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Validator for GetProductQuery that defines validation rules for retrieving a single product.
/// </summary>
public class GetProductValidator : AbstractValidator<GetProductQuery>
{
    /// <summary>
    /// Initializes a new instance of the GetProductValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must not be empty (default Guid)
    /// </remarks>
    public GetProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product Id is required and cannot be empty.");
    }
}