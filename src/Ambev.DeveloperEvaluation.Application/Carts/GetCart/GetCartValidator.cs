using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Validator for GetCartQuery that defines validation rules for retrieving a single cart.
/// </summary>
public class GetCartValidator : AbstractValidator<GetCartQuery>
{
    /// <summary>
    /// Initializes a new instance of the GetCartValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must not be empty (default Guid)
    /// </remarks>
    public GetCartValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Cart Id is required and cannot be empty.");
    }
}