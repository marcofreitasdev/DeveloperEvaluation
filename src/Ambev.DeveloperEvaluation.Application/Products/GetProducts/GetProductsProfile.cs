using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

/// <summary>
/// Profile for mapping between Product entity and GetProductsResult
/// </summary>
public class GetProductsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetProducts operation
    /// </summary>
    public GetProductsProfile()
    {
        CreateMap<Product, ProductResult>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating));

        CreateMap<Domain.Entities.ProductRating, ProductRatingResult>();

        CreateMap<(IEnumerable<Product> Products, int TotalCount, int Page, int PageSize), GetProductsResult>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Products))
            .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(src => src.TotalCount))
            .ForMember(dest => dest.CurrentPage, opt => opt.MapFrom(src => src.Page))
            .ForMember(dest => dest.TotalPages, opt => opt.MapFrom(src => (int)Math.Ceiling((double)src.TotalCount / src.PageSize)));
    }
}