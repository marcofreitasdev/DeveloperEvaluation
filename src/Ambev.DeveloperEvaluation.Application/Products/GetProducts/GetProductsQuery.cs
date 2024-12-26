using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

/// <summary>
/// Query for retrieving a list of products.
/// </summary>
/// <remarks>
/// This query is used to capture the required data for retrieving products,
/// including pagination, sorting parameters, and filtering options. It implements <see cref="IRequest{TResponse}"/>
/// to initiate the request that returns a <see cref="GetProductsResult"/>.
///
/// The data provided in this query is validated using the
/// <see cref="GetProductsValidator"/> which extends
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly
/// populated and follow the required business rules.
/// </remarks>
public class GetProductsQuery : IRequest<GetProductsResult>
{
    /// <summary>
    /// Gets or sets the page number for pagination.
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the number of items per page.
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Gets or sets the sorting order for the results.
    /// </summary>
    public string OrderBy { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the filters to be applied to the query.
    /// </summary>
    public Dictionary<string, string> Filters { get; set; } = [];

    public ValidationResultDetail Validate()
    {
        var validator = new GetProductsValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}