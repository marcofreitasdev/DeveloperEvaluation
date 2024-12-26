using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(200)
            .WithMessage("Title cannot be longer than 200 characters.");

        RuleFor(x => x.Price)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(1000)
            .WithMessage("Description cannot be longer than 1000 characters.");

        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("Category is required.")
            .MaximumLength(100)
            .WithMessage("Category cannot be longer than 100 characters.");

        RuleFor(x => x.Image)
            .NotEmpty()
            .WithMessage("Image URL is required.")
            .Must(BeAValidUrl)
            .WithMessage("Image must be a valid URL.");

        RuleFor(x => x.Rating)
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
        RuleFor(x => x.Rate)
            .InclusiveBetween(0, 5)
            .WithMessage("Rating rate must be between 0 and 5.");

        RuleFor(x => x.Count)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Rating count must be non-negative.");
    }
}