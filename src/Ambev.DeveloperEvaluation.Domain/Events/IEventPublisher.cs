using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Defines methods for publishing domain events.
/// </summary>
public interface IEventPublisher
{
    /// <summary>
    /// Publishes a SaleCreated event.
    /// </summary>
    /// <param name="saleId">The ID of the created sale.</param>
    Task PublishSaleCreatedEvent(Guid saleId);

    /// <summary>
    /// Publishes a SaleModified event.
    /// </summary>
    /// <param name="saleId">The ID of the modified sale.</param>
    Task PublishSaleModifiedEvent(Guid saleId);

    /// <summary>
    /// Publishes a SaleCancelled event.
    /// </summary>
    /// <param name="saleId">The ID of the cancelled sale.</param>
    Task PublishSaleCancelledEvent(Guid saleId);

    /// <summary>
    /// Publishes an ItemCancelled event.
    /// </summary>
    /// <param name="saleId">The ID of the sale.</param>
    /// <param name="itemId">The ID of the cancelled item.</param>
    Task PublishItemCancelledEvent(Guid saleId, Guid itemId);
}