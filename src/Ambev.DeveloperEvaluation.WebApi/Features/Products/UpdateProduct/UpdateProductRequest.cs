namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// Represents a request to edit an existing product in the system.
/// </summary>
public class UpdateProductRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the product to be edited.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the product. Must be unique and not empty.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the product. Must be greater than zero.
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
    /// Gets or sets the image URL of the product. Must be a valid URL.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the rating of the product.
    /// </summary>
    public UpdateProductRatingRequest Rating { get; set; } = new UpdateProductRatingRequest();
}

/// <summary>
/// Represents the rating information for a product in the edit request.
/// </summary>
public class UpdateProductRatingRequest
{
    /// <summary>
    /// Gets or sets the rate of the product rating. Must be between 0 and 5.
    /// </summary>
    public decimal Rate { get; set; }

    /// <summary>
    /// Gets or sets the count of ratings for the product. Must be non-negative.
    /// </summary>
    public int Count { get; set; }
}
