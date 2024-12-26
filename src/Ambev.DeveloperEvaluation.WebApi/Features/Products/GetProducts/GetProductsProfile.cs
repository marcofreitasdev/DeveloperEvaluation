using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.GetProducts;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;

/// <summary>
/// Profile for mapping between Application and API GetProducts responses
/// </summary>
public class GetProductsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetProducts feature
    /// </summary>
    public GetProductsProfile()
    {
        CreateMap<GetProductsRequest, GetProductsQuery>();

        CreateMap<GetProductsResult, GetProductsResponse>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));

        CreateMap<ProductResult, ProductResponse>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new ProductRatingResponse
            {
                Rate = src.Rating.Rate,
                Count = src.Rating.Count
            }));

        CreateMap<GetProductsRequest, GetProductsQuery>()
            .ForMember(dest => dest.Filters, opt => opt.MapFrom(src => src.Filters));

        CreateMap<ProductRatingResult, ProductRatingResponse>();
    }
}