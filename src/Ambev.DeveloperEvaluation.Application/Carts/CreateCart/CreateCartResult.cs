namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Represents the response after creating a new cart
/// </summary>
public sealed class CreateCartResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the created cart
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who created the cart
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the list of items in the cart
    /// </summary>
    public List<CreateCartItemResult> Items { get; set; } = [];

    /// <summary>
    /// Gets or sets the total price of the cart
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Gets or sets the total discount applied to the cart
    /// </summary>
    public decimal TotalDiscount { get; set; }

    /// <summary>
    /// Gets or sets the final price after applying discounts
    /// </summary>
    public decimal FinalPrice { get; set; }
}

/// <summary>
/// Represents an item in the cart result
/// </summary>
public sealed class CreateCartItemResult
{
    /// <summary>
    /// Gets or sets the ID of the product
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the title of the product
    /// </summary>
    public string ProductTitle { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity of the product in the cart
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the total price for this item (quantity * unit price)
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Gets or sets the discount applied to this item
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets or sets the final price for this item after applying the discount
    /// </summary>
    public decimal FinalPrice { get; set; }
}