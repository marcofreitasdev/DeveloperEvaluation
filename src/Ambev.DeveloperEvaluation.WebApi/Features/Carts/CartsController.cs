﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCarts;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCarts;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts;

/// <summary>
/// Controller for managing cart operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CartsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CartsController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CartsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new cart
    /// </summary>
    /// <param name="request">The cart creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created cart details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateCartResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCart([FromBody] CreateCartRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateCartCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateCartResponse>
        {
            Success = true,
            Message = "Cart created successfully",
            Data = _mapper.Map<CreateCartResponse>(response)
        });
    }

    /// <summary>
    /// Retrieves a list of carts
    /// </summary>
    /// <param name="request">The cart retrieval request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The list of carts and pagination details</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCartsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCarts([FromQuery] GetCartsRequest request, CancellationToken cancellationToken)
    {
        var validator = new GetCartsRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var query = _mapper.Map<GetCartsQuery>(request);
        var response = await _mediator.Send(query, cancellationToken);

        return Ok(new ApiResponseWithData<GetCartsResponse>
        {
            Success = true,
            Message = "Carts retrieved successfully",
            Data = _mapper.Map<GetCartsResponse>(response)
        });
    }

    /// <summary>
    /// Retrieves a cart by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCartResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCart([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCartQuery { Id = id };
        var response = await _mediator.Send(query, cancellationToken);

        if (response == null)
            return NotFound(new ApiResponse { Success = false, Message = "Cart not found" });

        return Ok(new ApiResponseWithData<GetCartResponse>
        {
            Success = true,
            Message = "Cart retrieved successfully",
            Data = _mapper.Map<GetCartResponse>(response)
        });
    }

    /// <summary>
    /// Updates an existing cart
    /// </summary>
    /// <param name="id">The unique identifier of the cart to update</param>
    /// <param name="request">The cart update request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated cart details</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateCartResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCart([FromRoute] Guid id, [FromBody] UpdateCartRequest request, CancellationToken cancellationToken)
    {
        var validator = new UpdateCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateCartCommand>(request);
        command.Id = id;
        var response = await _mediator.Send(command, cancellationToken);

        if (response == null)
            return NotFound(new ApiResponse { Success = false, Message = "Cart not found" });

        return Ok(new ApiResponseWithData<UpdateCartResponse>
        {
            Success = true,
            Message = "Cart updated successfully",
            Data = _mapper.Map<UpdateCartResponse>(response)
        });
    }

    /// <summary>
    /// Deletes a cart by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the cart to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the cart was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<DeleteCartResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCart([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCartCommand { Id = id };
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Success)
        {
            if (result.Message == "Cart not found.")
            {
                return NotFound(new ApiResponse { Success = false, Message = result.Message });
            }
            return BadRequest(new ApiResponse { Success = false, Message = result.Message });
        }

        var response = _mapper.Map<DeleteCartResponse>(result);
        return Ok(new ApiResponseWithData<DeleteCartResponse>
        {
            Success = true,
            Message = "Cart deleted successfully",
            Data = response
        });
    }
}