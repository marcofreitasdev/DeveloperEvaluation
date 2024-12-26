using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCarts;

/// <summary>
/// Query for retrieving a list of carts.
/// </summary>
/// <remarks>
/// This query is used to capture the required data for retrieving carts,
/// including pagination, sorting parameters, and filtering options. It implements <see cref="IRequest{TResponse}"/>
/// to initiate the request that returns a <see cref="GetCartsResult"/>.
///
/// The data provided in this query is validated using the
/// <see cref="GetCartsValidator"/> which extends
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly
/// populated and follow the required business rules.
/// </remarks>
public class GetCartsQuery : IRequest<GetCartsResult>
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
        var validator = new GetCartsValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}