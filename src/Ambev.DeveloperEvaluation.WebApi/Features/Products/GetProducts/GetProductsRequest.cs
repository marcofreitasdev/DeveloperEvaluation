using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;

/// <summary>
/// Represents a request to retrieve a list of products from the system.
/// </summary>
public class GetProductsRequest
{
    /// <summary>
    /// Gets or sets the filters to be applied to the query.
    /// </summary>
    [FromQuery(Name = "")]
    public Dictionary<string, string> Filters { get; set; } = [];

    /// <summary>
    /// Gets or sets the page number for pagination. Must be greater than 0.
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the number of items per page. Must be between 1 and 100.
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Gets or sets the sorting order for the results. 
    /// Format should be "propertyName [asc|desc]", e.g., "price desc".
    /// </summary>
    public string OrderBy { get; set; } = string.Empty;
}