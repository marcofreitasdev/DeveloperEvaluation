using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Handler for processing UpdateCartCommand requests
/// </summary>
public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, UpdateCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IEventPublisher _eventPublisher;

    /// <summary>
    /// Initializes a new instance of UpdateCartHandler
    /// </summary>
    /// <param name="cartRepository">The cart repository</param>
    /// <param name="productRepository">The product repository</param>  "
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="eventPublisher">The event publisher</param>
    public UpdateCartHandler(ICartRepository cartRepository, IProductRepository productRepository, IMapper mapper, IEventPublisher eventPublisher)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
        _productRepository = productRepository;
    }

    /// <summary>
    /// Handles the UpdateCartCommand request
    /// </summary>
    /// <param name="command">The UpdateCart command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The edited cart details</returns>
    public async Task<UpdateCartResult> Handle(UpdateCartCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateCartValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingCart = await _cartRepository.GetByIdAsync(command.Id, cancellationToken) ?? throw new InvalidOperationException($"Cart with id {command.Id} does not exist");
        existingCart.UserId = command.UserId;
        existingCart.Items.Clear();

        foreach (var item in command.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken) ?? throw new InvalidOperationException($"Product with id {item.ProductId} does not exist");
            var cartItem = new CartItem(item.ProductId, item.Quantity, product.Price);
            existingCart.Items.Add(cartItem);
        }

        var updatedCart = await _cartRepository.UpdateAsync(existingCart, cancellationToken);
        await _eventPublisher.PublishSaleModifiedEvent(updatedCart.Id);

        return _mapper.Map<UpdateCartResult>(updatedCart);
    }
}