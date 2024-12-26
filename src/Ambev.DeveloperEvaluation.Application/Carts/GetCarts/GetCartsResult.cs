namespace Ambev.DeveloperEvaluation.Application.Carts.GetCarts;

/// <summary>
/// Represents the response after retrieving a list of carts
/// </summary>
public sealed class GetCartsResult
{
    /// <summary>
    /// Gets or sets the list of carts
    /// </summary>
    public List<CartResult> Data { get; set; } = [];

    /// <summary>
    /// Gets or sets the total number of items across all pages
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    /// Gets or sets the current page number
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Gets or sets the total number of pages
    /// </summary>
    public int TotalPages { get; set; }
}

/// <summary>
/// Represents a cart in the result
/// </summary>
public sealed class CartResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who owns the cart
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the date when the cart was created
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the list of items in the cart
    /// </summary>
    public List<CartItemResult> Items { get; set; } = [];

    /// <summary>
    /// Gets or sets the total amount of the cart
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets whether the cart is cancelled
    /// </summary>
    public bool IsCancelled { get; set; }
}

/// <summary>
/// Represents an item in the cart result
/// </summary>
public sealed class CartItemResult
{
    /// <summary>
    /// Gets or sets the ID of the product
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product in the cart
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the total amount for this item
    /// </summary>
    public decimal TotalAmount { get; set; }
}
