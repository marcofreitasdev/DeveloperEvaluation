using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ICartRepository using Entity Framework Core
/// </summary>
public class CartRepository : ICartRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of CartRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public CartRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new cart in the database
    /// </summary>
    /// <param name="cart">The cart to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created cart</returns>
    public async Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        await _context.Carts.AddAsync(cart, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return cart;
    }

    /// <summary>
    /// Retrieves a cart by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart if found, null otherwise</returns>
    public async Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves all carts for a specific user
    /// </summary>
    /// <param name="userId">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of carts belonging to the user</returns>
    public async Task<IEnumerable<Cart>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Carts
            .Include(c => c.Items)
            .Where(c => c.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Updates an existing cart in the database
    /// </summary>
    /// <param name="cart">The cart to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated cart</returns>
    public async Task<Cart> UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        _context.Carts.Update(cart);
        await _context.SaveChangesAsync(cancellationToken);
        return cart;
    }

    /// <summary>
    /// Deletes a cart from the database
    /// </summary>
    /// <param name="id">The unique identifier of the cart to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the cart was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var cart = await GetByIdAsync(id, cancellationToken);
        if (cart == null)
            return false;

        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Retrieves a paginated list of carts
    /// </summary>
    /// <param name="page">The page number</param>
    /// <param name="pageSize">The number of items per page</param>
    /// <param name="orderBy">The ordering string</param>
    /// <param name="filters">The filters to apply</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A paginated list of carts</returns>
    public async Task<(IEnumerable<Cart> Carts, int TotalCount)> GetPaginatedAsync(
        int page,
        int pageSize,
        string orderBy,
        Dictionary<string, string> filters,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Carts.Include(c => c.Items).AsQueryable();

        foreach (var filter in filters)
        {
            switch (filter.Key.ToLower())
            {
                case "userid":
                    if (Guid.TryParse(filter.Value, out Guid userId))
                        query = query.Where(c => c.UserId == userId);
                    break;
                case "iscancelled":
                    if (bool.TryParse(filter.Value, out bool isCancelled))
                        query = query.Where(c => c.IsCancelled == isCancelled);
                    break;
            }
        }

        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            var orderParts = orderBy.Split(' ');
            var propertyName = orderParts[0];
            var direction = orderParts.Length > 1 ? orderParts[1] : "asc";

            switch (propertyName.ToLower())
            {
                case "date":
                    query = direction.ToLower() == "desc" ? query.OrderByDescending(c => c.Date) : query.OrderBy(c => c.Date);
                    break;
                case "totalamount":
                    query = direction.ToLower() == "desc" ? query.OrderByDescending(c => c.TotalAmount) : query.OrderBy(c => c.TotalAmount);
                    break;
                    // Add more ordering cases as needed
            }
        }

        var totalCount = await query.CountAsync(cancellationToken);
        var carts = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (carts, totalCount);
    }

    /// <summary>
    /// Cancels a cart
    /// </summary>
    /// <param name="id">The unique identifier of the cart to cancel</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cancelled cart if found and cancelled, null otherwise</returns>
    public async Task<Cart?> CancelCartAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var cart = await GetByIdAsync(id, cancellationToken);
        if (cart == null)
            return null;

        cart.Cancel();
        await _context.SaveChangesAsync(cancellationToken);
        return cart;
    }

    /// <summary>
    /// Adds a product to a cart
    /// </summary>
    /// <param name="cartId">The unique identifier of the cart</param>
    /// <param name="productId">The unique identifier of the product to add</param>
    /// <param name="quantity">The quantity of the product to add</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated cart</returns>
    public async Task<Cart> AddProductToCartAsync(Guid cartId, Guid productId, int quantity, CancellationToken cancellationToken = default)
    {
        var cart = await GetByIdAsync(cartId, cancellationToken) ?? throw new ArgumentException("Cart not found", nameof(cartId));
        var product = await _context.Products.FindAsync([productId], cancellationToken) ?? throw new ArgumentException("Product not found", nameof(productId));
        cart.AddProduct(productId, quantity, product.Price);
        await _context.SaveChangesAsync(cancellationToken);
        return cart;
    }

    /// <summary>
    /// Removes a product from a cart
    /// </summary>
    /// <param name="cartId">The unique identifier of the cart</param>
    /// <param name="productId">The unique identifier of the product to remove</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated cart</returns>
    public async Task<Cart> RemoveProductFromCartAsync(Guid cartId, Guid productId, CancellationToken cancellationToken = default)
    {
        var cart = await GetByIdAsync(cartId, cancellationToken) ?? throw new ArgumentException("Cart not found", nameof(cartId));
        cart.RemoveProduct(productId);
        await _context.SaveChangesAsync(cancellationToken);
        return cart;
    }
}