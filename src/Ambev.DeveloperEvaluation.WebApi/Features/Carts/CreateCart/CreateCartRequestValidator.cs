using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

/// <summary>
/// Validator for CreateCartRequest that defines validation rules for cart creation.
/// </summary>
public class CreateCartRequestValidator : AbstractValidator<CreateCartRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateCartRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - UserId: Must not be empty
    /// - Items: Must not be empty and each item must be valid
    /// </remarks>
    public CreateCartRequestValidator()
    {
        RuleFor(cart => cart.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(cart => cart.Items)
            .NotEmpty().WithMessage("Cart must contain at least one item.");

        RuleForEach(cart => cart.Items)
            .SetValidator(new CartItemRequestValidator());
    }
}

/// <summary>
/// Validator for CartItemRequest that defines validation rules for cart items.
/// </summary>
public class CartItemRequestValidator : AbstractValidator<CartItemRequest>
{
    /// <summary>
    /// Initializes a new instance of the CartItemRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - ProductId: Must not be empty
    /// - Quantity: Must be between 1 and 20
    /// </remarks>
    public CartItemRequestValidator()
    {
        RuleFor(item => item.ProductId)
            .NotEmpty().WithMessage("Product ID is required.");

        RuleFor(item => item.Quantity)
            .InclusiveBetween(1, 20).WithMessage("Quantity must be between 1 and 20.");
    }
}