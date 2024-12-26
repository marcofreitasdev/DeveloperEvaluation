using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Query for retrieving a single product by its ID.
/// </summary>
/// <remarks>
/// This query is used to capture the required data for retrieving a specific product.
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="GetProductResult"/>.
///
/// The data provided in this query is validated using the
/// <see cref="GetProductValidator"/> which extends
/// <see cref="AbstractValidator{T}"/> to ensure that the field is correctly
/// populated and follows the required business rules.
/// </remarks>
public class GetProductQuery : IRequest<GetProductResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the product to retrieve.
    /// </summary>
    public Guid Id { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new GetProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}