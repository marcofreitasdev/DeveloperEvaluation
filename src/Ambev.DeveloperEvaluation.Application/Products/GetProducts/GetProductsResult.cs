namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

/// <summary>
/// Represents the response after retrieving a list of products
/// </summary>
public sealed class GetProductsResult
{
    /// <summary>
    /// Gets or sets the list of products
    /// </summary>
    public List<ProductResult> Data { get; set; } = [];

    /// <summary>
    /// Gets or sets the total number of items across all pages
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    /// Gets or sets the current page number
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Gets or sets the total number of pages
    /// </summary>
    public int TotalPages { get; set; }
}

/// <summary>
/// Represents a product in the result
/// </summary>
public sealed class ProductResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the product
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
    public ProductRatingResult Rating { get; set; } = new ProductRatingResult();
}

/// <summary>
/// Represents the rating of a product in the result
/// </summary>
public sealed class ProductRatingResult
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