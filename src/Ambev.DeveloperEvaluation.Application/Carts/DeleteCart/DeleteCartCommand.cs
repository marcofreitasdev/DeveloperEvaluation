using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

/// <summary>
/// Command for deleting a specific cart.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for deleting a cart,
/// which is the cart's ID. It implements <see cref="IRequest{TResponse}"/>
/// to initiate the request that returns a <see cref="DeleteCartResult"/>.
///
/// The data provided in this command is validated using the
/// <see cref="DeleteCartValidator"/> which extends
/// <see cref="AbstractValidator{T}"/> to ensure that the field is correctly
/// populated and follows the required business rules.
/// </remarks>
public class DeleteCartCommand : IRequest<DeleteCartResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart to be deleted.
    /// </summary>
    public Guid Id { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new DeleteCartValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
