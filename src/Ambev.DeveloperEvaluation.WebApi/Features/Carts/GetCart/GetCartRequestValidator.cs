using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

/// <summary>
/// Validator for GetCartRequest that defines validation rules for retrieving a single cart.
/// </summary>
public class GetCartRequestValidator : AbstractValidator<GetCartRequest>
{
    /// <summary>
    /// Initializes a new instance of the GetCartRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must not be empty (default Guid)
    /// </remarks>
    public GetCartRequestValidator()
    {
        RuleFor(request => request.Id)
            .NotEmpty().WithMessage("Cart Id is required and cannot be empty.");
    }
}