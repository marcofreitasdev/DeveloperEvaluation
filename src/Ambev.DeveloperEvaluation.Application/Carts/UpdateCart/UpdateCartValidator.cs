using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Validator for UpdateCartCommand that defines validation rules for cart editing command.
/// </summary>
public class UpdateCartValidator : AbstractValidator<UpdateCartCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateCartValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must not be empty
    /// - UserId: Must not be empty
    /// - Items: Must not be empty and each item must be valid
    /// </remarks>
    public UpdateCartValidator()
    {
        RuleFor(cart => cart.Id)
            .NotEmpty().WithMessage("Cart Id is required.");

        RuleFor(cart => cart.UserId)
            .NotEmpty().WithMessage("User Id is required.");

        RuleFor(cart => cart.Items)
            .NotEmpty().WithMessage("Cart must contain at least one item.");

        RuleForEach(cart => cart.Items).SetValidator(new CartItemValidator());
    }
}

/// <summary>
/// Validator for CartItem that defines validation rules for cart items.
/// </summary>
public class CartItemValidator : AbstractValidator<UpdateCartItem>
{
    /// <summary>
    /// Initializes a new instance of the CartItemValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - ProductId: Must not be empty
    /// - Quantity: Must be between 1 and 20
    /// </remarks>
    public CartItemValidator()
    {
        RuleFor(item => item.ProductId)
            .NotEmpty().WithMessage("Product Id is required.");

        RuleFor(item => item.Quantity)
            .InclusiveBetween(1, 20).WithMessage("Quantity must be between 1 and 20.");
    }
}