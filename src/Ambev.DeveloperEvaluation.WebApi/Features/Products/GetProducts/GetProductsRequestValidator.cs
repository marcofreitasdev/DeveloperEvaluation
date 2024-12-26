using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;

/// <summary>
/// Validator for GetProductsRequest that defines validation rules for product retrieval.
/// </summary>
public class GetProductsRequestValidator : AbstractValidator<GetProductsRequest>
{
    /// <summary>
    /// Initializes a new instance of the GetProductsRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Page: Must be greater than 0
    /// - PageSize: Must be between 1 and 100
    /// - OrderBy: Must be a valid ordering string (optional)
    /// </remarks>
    public GetProductsRequestValidator()
    {
        RuleFor(request => request.Page)
            .GreaterThan(0).WithMessage("Page number must be greater than 0.");

        RuleFor(request => request.PageSize)
            .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100.");

        RuleFor(request => request.OrderBy)
            .Must(BeAValidOrderByString)
            .When(request => !string.IsNullOrEmpty(request.OrderBy))
            .WithMessage("Invalid OrderBy string. Use format: 'property [asc|desc]'.");
    }

    /// <summary>
    /// Validates if the provided string is a valid OrderBy clause.
    /// </summary>
    /// <param name="orderBy">The OrderBy string to validate.</param>
    /// <returns>True if the OrderBy string is valid; otherwise, false.</returns>
    private bool BeAValidOrderByString(string orderBy)
    {
        if (string.IsNullOrWhiteSpace(orderBy))
            return true;

        var parts = orderBy.Split(' ');
        if (parts.Length > 2)
            return false;

        if (parts.Length == 2 && !parts[1].Equals("asc", StringComparison.CurrentCultureIgnoreCase) && !parts[1].Equals("desc", StringComparison.CurrentCultureIgnoreCase))
            return false;

        // You might want to add additional checks here to ensure the property name is valid
        // This could involve checking against a list of allowed property names

        return true;
    }
}