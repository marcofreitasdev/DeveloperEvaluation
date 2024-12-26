using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Events;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Common.Exceptions;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="CreateCartHandler"/> class.
/// </summary>
public class CreateCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IEventPublisher _eventPublisher;
    private readonly CreateCartHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCartHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public CreateCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _eventPublisher = Substitute.For<IEventPublisher>();
        _handler = new CreateCartHandler(_cartRepository, _productRepository, _mapper, _eventPublisher);
    }

    /// <summary>
    /// Tests that a valid cart creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid cart data When creating cart Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = new CreateCartCommand
        {
            UserId = Guid.NewGuid(),
            Items =
            [
                new CreateCartItem { ProductId = Guid.NewGuid(), Quantity = 2 }
            ]
        };

        var product = new Product { Id = command.Items[0].ProductId, Price = 10.0m };
        var cart = new Cart { Id = Guid.NewGuid(), UserId = command.UserId };
        cart.AddProduct(product.Id, command.Items[0].Quantity, product.Price);

        var result = new CreateCartResult { Id = cart.Id };

        _productRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(product);
        _cartRepository.CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>()).Returns(cart);
        _mapper.Map<CreateCartResult>(Arg.Any<Cart>()).Returns(result);

        // When
        var createCartResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createCartResult.Should().NotBeNull();
        createCartResult.Id.Should().Be(cart.Id);
        await _cartRepository.Received(1).CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());
        await _eventPublisher.Received(1).PublishSaleCreatedEvent(cart.Id);
    }

    /// <summary>
    /// Tests that an invalid cart creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid cart data When creating cart Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateCartCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that a not found product throws NotFoundException.
    /// </summary>
    [Fact(DisplayName = "Given non-existent product When creating cart Then throws NotFoundException")]
    public async Task Handle_NonExistentProduct_ThrowsNotFoundException()
    {
        // Given
        var command = new CreateCartCommand
        {
            UserId = Guid.NewGuid(),
            Items =
            [
                new CreateCartItem { ProductId = Guid.NewGuid(), Quantity = 2 }
            ]
        };

        _productRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns((Product)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<NotFoundException>();
    }

    /// <summary>
    /// Tests that the mapper is called with the correct cart.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps cart to result")]
    public async Task Handle_ValidRequest_MapsCartToResult()
    {
        // Given
        var command = new CreateCartCommand
        {
            UserId = Guid.NewGuid(),
            Items =
            [
                new CreateCartItem { ProductId = Guid.NewGuid(), Quantity = 2 }
            ]
        };

        var product = new Product { Id = command.Items[0].ProductId, Price = 10.0m };
        var cart = new Cart { Id = Guid.NewGuid(), UserId = command.UserId };
        cart.AddProduct(product.Id, command.Items[0].Quantity, product.Price);

        _productRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(product);
        _cartRepository.CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>()).Returns(cart);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<CreateCartResult>(Arg.Is<Cart>(c =>
            c.Id == cart.Id &&
            c.UserId == command.UserId &&
            c.Items.Count == 1 &&
            c.Items[0].ProductId == command.Items[0].ProductId &&
            c.Items[0].Quantity == command.Items[0].Quantity));
    }
}