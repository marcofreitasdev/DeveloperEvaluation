namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

/// <summary>
/// Represents a request to edit an existing cart in the system.
/// </summary>
public class UpdateCartRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart to be edited.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who owns the cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the list of items in the cart.
    /// </summary>
    public List<UpdateCartItemRequest> Items { get; set; } = [];
}

/// <summary>
/// Represents an item in the cart update request.
/// </summary>
public class UpdateCartItemRequest
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
