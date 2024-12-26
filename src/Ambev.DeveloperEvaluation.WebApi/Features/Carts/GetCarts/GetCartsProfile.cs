using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Carts.GetCarts;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCarts;

/// <summary>
/// Profile for mapping between Application and API GetCarts responses
/// </summary>
public class GetCartsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetCarts feature
    /// </summary>
    public GetCartsProfile()
    {
        CreateMap<GetCartsRequest, GetCartsQuery>();

        CreateMap<GetCartsResult, GetCartsResponse>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));

        CreateMap<CartResult, CartResponse>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<GetCartsRequest, GetCartsQuery>()
            .ForMember(dest => dest.Filters, opt => opt.MapFrom(src => src.Filters));

        CreateMap<CartItemResult, CartItemResponse>();
    }
}