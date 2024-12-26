using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class CartValidator : AbstractValidator<Cart>
{
    public CartValidator()
    {
        RuleFor(cart => cart.UserId)
            .NotNull()
            .WithMessage("UserId must be a value.");

        RuleFor(cart => cart.Date)
            .NotEmpty()
            .WithMessage("Date is required.");

        RuleFor(cart => cart.Items)
            .NotEmpty()
            .WithMessage("Cart must contain at least one item.");

        RuleForEach(cart => cart.Items)
            .SetValidator(new CartItemValidator());

        RuleFor(cart => cart.TotalAmount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Total amount cannot be negative.");

        RuleFor(cart => cart.Items)
            .Must(items => items.All(item => item.Quantity <= 20))
            .WithMessage("It's not possible to sell above 20 identical items.");

        RuleFor(cart => cart.Items)
            .Must(items => items.All(item => item.Quantity >= 4 || item.Quantity == 0 || item.Discount == 0))
            .WithMessage("Purchases below 4 items cannot have a discount.");
    }
}

public class CartItemValidator : AbstractValidator<CartItem>
{
    public CartItemValidator()
    {
        RuleFor(item => item.ProductId)
            .NotNull()
            .WithMessage("ProductId must be a value.");

        RuleFor(item => item.Quantity)
            .GreaterThan(0)
            .LessThanOrEqualTo(20)
            .WithMessage("Quantity must be between 1 and 20.");

        RuleFor(item => item.UnitPrice)
            .GreaterThan(0)
            .WithMessage("Unit price must be greater than zero.");

        RuleFor(item => item.Discount)
            .InclusiveBetween(0, 0.2m)
            .WithMessage("Discount must be between 0% and 20%.");

        RuleFor(item => item.TotalAmount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Total amount cannot be negative.");

        RuleFor(item => item)
            .Must(item => item.TotalAmount == item.Quantity * item.UnitPrice * (1 - item.Discount))
            .WithMessage("Total amount calculation is incorrect.");
    }
}