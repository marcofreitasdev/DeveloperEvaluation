﻿namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;

/// <summary>
/// Represents the response after deleting a cart from the system.
/// </summary>
public class DeleteCartResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the deleted cart.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the deletion was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets a message describing the result of the deletion operation.
    /// </summary>
    public string Message { get; set; } = string.Empty;
}