using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

/// <summary>
/// Validator for GetProductsCommand that defines validation rules for retrieving products.
/// </summary>
public class GetProductsValidator : AbstractValidator<GetProductsQuery>
{
    /// <summary>
    /// Initializes a new instance of the GetProductsCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Page: Must be greater than 0
    /// - PageSize: Must be between 1 and 100
    /// - OrderBy: Must be a valid ordering string (optional)
    /// </remarks>
    public GetProductsValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0)
            .WithMessage("Page number must be greater than 0.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100)
            .WithMessage("Page size must be between 1 and 100.");

        RuleFor(x => x.OrderBy)
            .Must(BeAValidOrderByString)
            .When(x => !string.IsNullOrEmpty(x.OrderBy))
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

        return true;
    }
}