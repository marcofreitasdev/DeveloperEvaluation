using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Events;
using AutoMapper;
using FluentValidation;
using MediatR;
using Ambev.DeveloperEvaluation.Common.Exceptions;
using FluentValidation.Results;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Handler for processing CreateCartCommand requests
/// </summary>
public class CreateCartHandler : IRequestHandler<CreateCartCommand, CreateCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IEventPublisher _eventPublisher;

    /// <summary>
    /// Initializes a new instance of CreateCartHandler
    /// </summary>
    /// <param name="cartRepository">The cart repository</param>
    /// <param name="productRepository">The product repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="eventPublisher">The event publisher</param>
    public CreateCartHandler(ICartRepository cartRepository, IProductRepository productRepository, IMapper mapper, IEventPublisher eventPublisher)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
    }

    /// <summary>
    /// Handles the CreateCartCommand request
    /// </summary>
    /// <param name="command">The CreateCart command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created cart details</returns>
    public async Task<CreateCartResult> Handle(CreateCartCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateCartValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var cart = new Cart { UserId = command.UserId };

        foreach (var item in command.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken) ?? throw new NotFoundException($"Product with ID {item.ProductId} not found.");

            cart.AddProduct(product.Id, item.Quantity, product.Price);
        }

        var cartValidationResult = cart.Validate();
        if (!cartValidationResult.IsValid)
            throw new ValidationException(cartValidationResult.Errors.Select(e => new ValidationFailure(e.Error, e.Detail)));

        var createdCart = await _cartRepository.CreateAsync(cart, cancellationToken);

        await _eventPublisher.PublishSaleCreatedEvent(createdCart.Id);

        var result = _mapper.Map<CreateCartResult>(createdCart);
        return result;
    }
}