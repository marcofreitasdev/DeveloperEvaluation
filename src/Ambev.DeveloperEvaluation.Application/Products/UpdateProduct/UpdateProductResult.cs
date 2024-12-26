namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Represents the response returned after successfully editing a product.
/// </summary>
/// <remarks>
/// This response contains the details of the edited product,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class UpdateProductResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the edited product.
    /// </summary>
    /// <value>A GUID that uniquely identifies the edited product in the system.</value>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the edited product.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the edited product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the description of the edited product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the edited product.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the image URL of the edited product.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the rating of the edited product.
    /// </summary>
    public UpdateProductRatingResult Rating { get; set; } = new UpdateProductRatingResult();
}

public sealed class UpdateProductRatingResult
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