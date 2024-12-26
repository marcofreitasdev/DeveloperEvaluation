using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCarts;

/// <summary>
/// Handler for processing GetCartsQuery requests
/// </summary>
public class GetCartsHandler : IRequestHandler<GetCartsQuery, GetCartsResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetCartsHandler
    /// </summary>
    /// <param name="cartRepository">The cart repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public GetCartsHandler(ICartRepository cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetCartsQuery request
    /// </summary>
    /// <param name="query">The GetCarts query</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The list of carts and pagination details</returns>
    public async Task<GetCartsResult> Handle(GetCartsQuery query, CancellationToken cancellationToken)
    {
        var validator = new GetCartsValidator();
        var validationResult = await validator.ValidateAsync(query, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var filters = new Dictionary<string, string>();
        foreach (var filter in query.Filters)
        {
            filters.Add(filter.Key, filter.Value);
        }

        var (carts, totalCount) = await _cartRepository.GetPaginatedAsync(query.Page, query.PageSize, query.OrderBy, filters, cancellationToken);

        var result = _mapper.Map<GetCartsResult>((carts, totalCount, query.Page, query.PageSize));
        return result;
    }
}