namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

/// <summary>
/// Represents a request to create a new cart in the system.
/// </summary>
public class CreateCartRequest
{
    /// <summary>
    /// Gets or sets the ID of the user creating the cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the list of items in the cart.
    /// </summary>
    public List<CartItemRequest> Items { get; set; } = [];
}

/// <summary>
/// Represents an item in the cart creation request.
/// </summary>
public class CartItemRequest
{
    /// <summary>
    /// Gets or sets the ID of the product. Must be a valid product ID in the system.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product. Must be between 1 and 20.
    /// </summary>
    public int Quantity { get; set; }
}