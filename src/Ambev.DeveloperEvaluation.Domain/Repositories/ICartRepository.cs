using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Cart entity operations
/// </summary>
public interface ICartRepository
{
    /// <summary>
    /// Creates a new cart in the repository
    /// </summary>
    /// <param name="cart">The cart to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created cart</returns>
    Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a cart by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart if found, null otherwise</returns>
    Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all carts for a specific user
    /// </summary>
    /// <param name="userId">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of carts belonging to the user</returns>
    Task<IEnumerable<Cart>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing cart in the repository
    /// </summary>
    /// <param name="cart">The cart to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated cart</returns>
    Task<Cart> UpdateAsync(Cart cart, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a cart from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the cart to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the cart was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a paginated list of carts
    /// </summary>
    /// <param name="page">The page number</param>
    /// <param name="pageSize">The number of items per page</param>
    /// <param name="orderBy">The ordering string</param>
    /// <param name="filters">The filters to apply</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A paginated list of carts</returns>
    Task<(IEnumerable<Cart> Carts, int TotalCount)> GetPaginatedAsync(int page, int pageSize, string orderBy, Dictionary<string, string> filters, CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancels a cart
    /// </summary>
    /// <param name="id">The unique identifier of the cart to cancel</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cancelled cart if found and cancelled, null otherwise</returns>
    Task<Cart?> CancelCartAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a product to a cart
    /// </summary>
    /// <param name="cartId">The unique identifier of the cart</param>
    /// <param name="productId">The unique identifier of the product to add</param>
    /// <param name="quantity">The quantity of the product to add</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated cart</returns>
    Task<Cart> AddProductToCartAsync(Guid cartId, Guid productId, int quantity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes a product from a cart
    /// </summary>
    /// <param name="cartId">The unique identifier of the cart</param>
    /// <param name="productId">The unique identifier of the product to remove</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated cart</returns>
    Task<Cart> RemoveProductFromCartAsync(Guid cartId, Guid productId, CancellationToken cancellationToken = default);
}