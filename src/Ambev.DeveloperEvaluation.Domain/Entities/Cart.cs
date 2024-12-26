using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.Results;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a shopping cart in the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Cart : BaseEntity
{
    /// <summary>
    /// Gets or sets the ID of the user who owns the cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the date when the cart was created.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the list of products in the cart.
    /// </summary>
    public List<CartItem> Items { get; set; } = [];

    /// <summary>
    /// Gets or sets the total amount of the cart.
    /// </summary>
    public decimal TotalAmount { get; private set; }

    /// <summary>
    /// Gets or sets whether the cart is cancelled.
    /// </summary>
    public bool IsCancelled { get; private set; }

    /// <summary>
    /// Initializes a new instance of the Cart class.
    /// </summary>
    public Cart()
    {
        Date = DateTime.UtcNow;
    }

    /// <summary>
    /// Adds a product to the cart or updates its quantity if it already exists.
    /// </summary>
    /// <param name="productId">The ID of the product to add.</param>
    /// <param name="quantity">The quantity to add.</param>
    /// <param name="unitPrice">The unit price of the product.</param>
    public void AddProduct(Guid productId, int quantity, decimal unitPrice)
    {
        if (quantity <= 0 || quantity > 20)
            throw new ArgumentException("Quantity must be between 1 and 20.");

        var existingItem = Items.FirstOrDefault(i => i.ProductId == productId);
        if (existingItem != null)
        {
            existingItem.UpdateQuantity(existingItem.Quantity + quantity);
        }
        else
        {
            Items.Add(new CartItem(productId, quantity, unitPrice));
        }

        CalculateTotalAmount();
    }

    /// <summary>
    /// Removes a product from the cart.
    /// </summary>
    /// <param name="productId">The ID of the product to remove.</param>
    public void RemoveProduct(Guid productId)
    {
        var item = Items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            Items.Remove(item);
            CalculateTotalAmount();
        }
    }

    /// <summary>
    /// Cancels the cart.
    /// </summary>
    public void Cancel()
    {
        IsCancelled = true;
    }

    /// <summary>
    /// Calculates the total amount of the cart, applying discounts where applicable.
    /// </summary>
    private void CalculateTotalAmount()
    {
        TotalAmount = Items.Sum(item => item.TotalAmount);
    }

    /// <summary>
    /// Performs validation of the cart entity using the CartValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new CartValidator();
        var result = validator.Validate(this);

        if (Items.Count == 0)
        {
            result.Errors.Add(new ValidationFailure(nameof(Items), "Cart must contain at least one item."));
        }

        return new ValidationResultDetail
        {
            IsValid = result.IsValid && Items.Count > 0,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

}

/// <summary>
/// Represents an item in the shopping cart.
/// </summary>
public class CartItem
{
    /// <summary>
    /// Gets the ID of the product.
    /// </summary>
    public Guid ProductId { get; }

    /// <summary>
    /// Gets the quantity of the product.
    /// </summary>
    public int Quantity { get; private set; }

    /// <summary>
    /// Gets the unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; }

    /// <summary>
    /// Gets the discount applied to this item.
    /// </summary>
    public decimal Discount { get; private set; }

    /// <summary>
    /// Gets the total amount for this item (including discount).
    /// </summary>
    public decimal TotalAmount { get; private set; }

    /// <summary>
    /// Initializes a new instance of the CartItem class.
    /// </summary>
    public CartItem(Guid productId, int quantity, decimal unitPrice)
    {
        ProductId = productId;
        UnitPrice = unitPrice;
        UpdateQuantity(quantity);
    }

    /// <summary>
    /// Updates the quantity of the item and recalculates the discount and total amount.
    /// </summary>
    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0 || newQuantity > 20)
            throw new ArgumentException("Quantity must be between 1 and 20.");

        Quantity = newQuantity;
        CalculateDiscount();
        CalculateTotalAmount();
    }

    /// <summary>
    /// Calculates the discount based on the quantity.
    /// </summary>
    private void CalculateDiscount()
    {
        if (Quantity >= 10 && Quantity <= 20)
            Discount = 0.2m; // for 20% discount
        else if (Quantity >= 4)
            Discount = 0.1m; // for 10% discount
        else
            Discount = 0;
    }

    /// <summary>
    /// Calculates the total amount for this item, applying the discount.
    /// </summary>
    private void CalculateTotalAmount()
    {
        TotalAmount = Quantity * UnitPrice * (1 - Discount);
    }
}