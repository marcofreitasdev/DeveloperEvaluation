using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Query for retrieving a single cart by its ID.
/// </summary>
/// <remarks>
/// This query is used to capture the required data for retrieving a specific cart.
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="GetCartResult"/>.
///
/// The data provided in this query is validated using the
/// <see cref="GetCartValidator"/> which extends
/// <see cref="AbstractValidator{T}"/> to ensure that the field is correctly
/// populated and follows the required business rules.
/// </remarks>
public class GetCartQuery : IRequest<GetCartResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart to retrieve.
    /// </summary>
    public Guid Id { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new GetCartValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}