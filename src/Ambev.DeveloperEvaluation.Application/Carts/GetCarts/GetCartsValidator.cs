﻿using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCarts;

/// <summary>
/// Validator for GetCartsQuery that defines validation rules for retrieving carts.
/// </summary>
public class GetCartsValidator : AbstractValidator<GetCartsQuery>
{
    /// <summary>
    /// Initializes a new instance of the GetCartsValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Page: Must be greater than 0
    /// - PageSize: Must be between 1 and 100
    /// - OrderBy: Must be a valid ordering string (optional)
    /// </remarks>
    public GetCartsValidator()
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
