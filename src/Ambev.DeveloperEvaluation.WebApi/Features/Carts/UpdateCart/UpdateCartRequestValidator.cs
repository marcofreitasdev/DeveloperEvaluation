using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

/// <summary>
/// Validator for UpdateCartRequest that defines validation rules for cart editing.
/// </summary>
public class UpdateCartRequestValidator : AbstractValidator<UpdateCartRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateCartRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must not be empty
    /// - UserId: Must not be empty
    /// - Items: Must not be empty and each item must be valid
    /// </remarks>
    public UpdateCartRequestValidator()
    {
        RuleFor(cart => cart.Id)
            .NotEmpty().WithMessage("Cart Id is required.");

        RuleFor(cart => cart.UserId)
            .NotEmpty().WithMessage("User Id is required.");

        RuleFor(cart => cart.Items)
            .NotEmpty().WithMessage("Cart must contain at least one item.");

        RuleForEach(cart => cart.Items).SetValidator(new UpdateCartItemValidator());
    }
}

/// <summary>
/// Validator for UpdateCartItemRequest that defines validation rules for cart items.
/// </summary>
public class UpdateCartItemValidator : AbstractValidator<UpdateCartItemRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateCartItemValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - ProductId: Must not be empty
    /// - Quantity: Must be between 1 and 20
    /// </remarks>
    public UpdateCartItemValidator()
    {
        RuleFor(item => item.ProductId)
            .NotEmpty().WithMessage("Product Id is required.");

        RuleFor(item => item.Quantity)
            .InclusiveBetween(1, 20).WithMessage("Quantity must be between 1 and 20.");
    }
}