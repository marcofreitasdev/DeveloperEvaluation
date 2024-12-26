namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Represents the response returned after successfully editing a cart.
/// </summary>
/// <remarks>
/// This response contains the details of the edited cart,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class UpdateCartResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the edited cart.
    /// </summary>
    /// <value>A GUID that uniquely identifies the edited cart in the system.</value>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who owns the cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the date when the cart was last updated.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the list of items in the edited cart.
    /// </summary>
    public List<UpdateCartItemResult> Items { get; set; } = [];

    /// <summary>
    /// Gets or sets the total amount of the edited cart.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets whether the edited cart is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}

/// <summary>
/// Represents an item in the edited cart result.
/// </summary>
public sealed class UpdateCartItemResult
{
    /// <summary>
    /// Gets or sets the ID of the product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product in the cart.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the total amount for this item.
    /// </summary>
    public decimal TotalAmount { get; set; }
}