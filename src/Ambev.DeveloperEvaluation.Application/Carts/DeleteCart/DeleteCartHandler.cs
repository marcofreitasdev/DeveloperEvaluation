using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

/// <summary>
/// Handler for processing DeleteCartCommand requests
/// </summary>
public class DeleteCartHandler : IRequestHandler<DeleteCartCommand, DeleteCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly IEventPublisher _eventPublisher;

    /// <summary>
    /// Initializes a new instance of DeleteCartHandler
    /// </summary>
    /// <param name="cartRepository">The cart repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="eventPublisher">The event publisher</param>
    public DeleteCartHandler(ICartRepository cartRepository, IMapper mapper, IEventPublisher eventPublisher)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
    }

    /// <summary>
    /// Handles the DeleteCartCommand request
    /// </summary>
    /// <param name="command">The DeleteCart command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task<DeleteCartResult> Handle(DeleteCartCommand command, CancellationToken cancellationToken)
    {
        var validator = new DeleteCartValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var cart = await _cartRepository.GetByIdAsync(command.Id, cancellationToken);

        if (cart == null)
        {
            return new DeleteCartResult
            {
                Id = command.Id,
                Success = false,
                Message = "Cart not found."
            };
        }

        var deleted = await _cartRepository.DeleteAsync(command.Id, cancellationToken);

        if (deleted)
        {
            await _eventPublisher.PublishSaleCancelledEvent(command.Id);
        }

        return new DeleteCartResult
        {
            Id = command.Id,
            Success = deleted,
            Message = deleted ? "Cart successfully deleted." : "Failed to delete cart."
        };
    }
}