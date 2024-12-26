using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(product => product.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(200)
            .WithMessage("Title cannot be longer than 200 characters.");

        RuleFor(product => product.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero.");

        RuleFor(product => product.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(1000)
            .WithMessage("Description cannot be longer than 1000 characters.");

        RuleFor(product => product.Category)
            .NotEmpty()
            .WithMessage("Category is required.")
            .MaximumLength(100)
            .WithMessage("Category cannot be longer than 100 characters.");

        RuleFor(product => product.Image)
            .NotEmpty()
            .WithMessage("Image URL is required.")
            .Must(BeAValidUrl)
            .WithMessage("Image must be a valid URL.");

        RuleFor(product => product.Rating)
            .SetValidator(new ProductRatingValidator());
    }

    private bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}

public class ProductRatingValidator : AbstractValidator<ProductRating>
{
    public ProductRatingValidator()
    {
        RuleFor(rating => rating.Rate)
            .InclusiveBetween(0, 5)
            .WithMessage("Rating rate must be between 0 and 5.");

        RuleFor(rating => rating.Count)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Rating count must be non-negative.");
    }
}
