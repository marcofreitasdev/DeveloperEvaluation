using FluentValidation;
using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// Validator for GetProductRequest that defines validation rules for retrieving a single product.
/// </summary>
public class GetProductRequestValidator : AbstractValidator<GetProductRequest>
{
    /// <summary>
    /// Initializes a new instance of the GetProductRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must not be empty (default Guid)
    /// </remarks>
    public GetProductRequestValidator()
    {
        RuleFor(request => request.Id)
            .NotEmpty().WithMessage("Product Id is required and cannot be empty.");
    }
}