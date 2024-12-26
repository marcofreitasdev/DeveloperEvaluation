namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// Represents the response after editing an existing product in the system.
/// </summary>
public class UpdateProductResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the edited product.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the product.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the product.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the image URL of the product.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the rating of the product.
    /// </summary>
    public UpdateProductRatingResponse Rating { get; set; } = new UpdateProductRatingResponse();
}

/// <summary>
/// Represents the rating information for a product in the edit response.
/// </summary>
public class UpdateProductRatingResponse
{
    /// <summary>
    /// Gets or sets the rate of the product rating.
    /// </summary>
    public decimal Rate { get; set; }

    /// <summary>
    /// Gets or sets the count of ratings for the product.
    /// </summary>
    public int Count { get; set; }
}