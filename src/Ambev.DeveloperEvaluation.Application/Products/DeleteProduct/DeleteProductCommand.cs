using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

/// <summary>
/// Command for deleting a specific product.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for deleting a product,
/// which is the product's ID. It implements <see cref="IRequest{TResponse}"/>
/// to initiate the request that returns a <see cref="DeleteProductResult"/>.
///
/// The data provided in this command is validated using the
/// <see cref="DeleteProductValidator"/> which extends
/// <see cref="AbstractValidator{T}"/> to ensure that the field is correctly
/// populated and follows the required business rules.
/// </remarks>
public class DeleteProductCommand : IRequest<DeleteProductResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the product to be deleted.
    /// </summary>
    public Guid Id { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new DeleteProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}