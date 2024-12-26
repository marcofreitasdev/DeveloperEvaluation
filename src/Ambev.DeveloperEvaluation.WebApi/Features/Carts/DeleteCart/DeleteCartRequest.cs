namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;

/// <summary>
/// Represents a request to delete a cart from the system.
/// </summary>
public class DeleteCartRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart to be deleted.
    /// </summary>
    public Guid Id { get; set; }
}