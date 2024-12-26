﻿namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Represents the response after retrieving a single cart
/// </summary>
public sealed class GetCartResult
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
    public List<GetCartItemResult> Items { get; set; } = [];

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
public sealed class GetCartItemResult
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