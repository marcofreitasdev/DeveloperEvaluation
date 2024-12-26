namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Represents the response after creating a new product
/// </summary>
public sealed class CreateProductResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the created product
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the product
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the product
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the description of the product
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the product
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the image URL of the product
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the rating of the product
    /// </summary>
    public CreateProductRatingResult Rating { get; set; } = new CreateProductRatingResult();
}

/// <summary>
/// Represents the rating of a product in the result
/// </summary>
public sealed class CreateProductRatingResult
{
    /// <summary>
    /// Gets or sets the rate of the product rating
    /// </summary>
    public decimal Rate { get; set; }

    /// <summary>
    /// Gets or sets the count of ratings for the product
    /// </summary>
    public int Count { get; set; }
}