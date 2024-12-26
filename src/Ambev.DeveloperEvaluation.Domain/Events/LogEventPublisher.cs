using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class LogEventPublisher : IEventPublisher
    {
        private readonly ILogger<LogEventPublisher> _logger;

        public LogEventPublisher(ILogger<LogEventPublisher> logger)
        {
            _logger = logger;
        }

        public Task PublishSaleCreatedEvent(Guid saleId)
        {
            _logger.LogInformation("SaleCreated event published. SaleId: {SaleId}", saleId);
            return Task.CompletedTask;
        }

        public Task PublishSaleModifiedEvent(Guid saleId)
        {
            _logger.LogInformation("SaleModified event published. SaleId: {SaleId}", saleId);
            return Task.CompletedTask;
        }

        public Task PublishSaleCancelledEvent(Guid saleId)
        {
            _logger.LogInformation("SaleCancelled event published. SaleId: {SaleId}", saleId);
            return Task.CompletedTask;
        }

        public Task PublishItemCancelledEvent(Guid saleId, Guid itemId)
        {
            _logger.LogInformation("ItemCancelled event published. SaleId: {SaleId}, ItemId: {ItemId}", saleId, itemId);
            return Task.CompletedTask;
        }
    }
}