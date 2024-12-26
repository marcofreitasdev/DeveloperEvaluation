using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

/// <summary>
/// Handler for processing GetProductsCommand requests
/// </summary>
public class GetProductsHandler : IRequestHandler<GetProductsQuery, GetProductsResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetProductsHandler
    /// </summary>
    /// <param name="productRepository">The product repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public GetProductsHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetProductsCommand request
    /// </summary>
    /// <param name="command">The GetProducts command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The list of products and pagination details</returns>
    public async Task<GetProductsResult> Handle(GetProductsQuery command, CancellationToken cancellationToken)
    {
        var validator = new GetProductsValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var filters = new Dictionary<string, string>();
        foreach (var filter in command.Filters)
        {
            filters.Add(filter.Key, filter.Value);
        }

        var (products, totalCount) = await _productRepository.GetPaginatedAsync(command.Page, command.PageSize, command.OrderBy, filters, cancellationToken);

        var result = _mapper.Map<GetProductsResult>((products, totalCount, command.Page, command.PageSize));
        return result;
    }
}