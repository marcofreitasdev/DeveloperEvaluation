using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Validator for CreateProductRequest that defines validation rules for product creation.
/// </summary>
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateProductRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Title: Required, length between 1 and 200 characters
    /// - Price: Must be greater than zero
    /// - Description: Required, maximum length of 1000 characters
    /// - Category: Required, maximum length of 100 characters
    /// - Image: Must be a valid URL
    /// - Rating: Must be valid (using ProductRatingValidator)
    /// </remarks>
    public CreateProductRequestValidator()
    {
        RuleFor(product => product.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title cannot be longer than 200 characters.");

        RuleFor(product => product.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(product => product.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(1000).WithMessage("Description cannot be longer than 1000 characters.");

        RuleFor(product => product.Category)
            .NotEmpty().WithMessage("Category is required.")
            .MaximumLength(100).WithMessage("Category cannot be longer than 100 characters.");

        RuleFor(product => product.Image)
            .NotEmpty().WithMessage("Image URL is required.")
            .Must(BeAValidUrl).WithMessage("Image must be a valid URL.");

        RuleFor(product => product.Rating).SetValidator(new ProductRatingValidator());
    }

    private bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}

/// <summary>
/// Validator for ProductRatingRequest that defines validation rules for product rating.
/// </summary>
public class ProductRatingValidator : AbstractValidator<ProductRatingRequest>
{
    /// <summary>
    /// Initializes a new instance of the ProductRatingValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Rate: Must be between 0 and 5
    /// - Count: Must be non-negative
    /// </remarks>
    public ProductRatingValidator()
    {
        RuleFor(rating => rating.Rate)
            .InclusiveBetween(0, 5).WithMessage("Rating rate must be between 0 and 5.");

        RuleFor(rating => rating.Count)
            .GreaterThanOrEqualTo(0).WithMessage("Rating count must be non-negative.");
    }
}