using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCarts;

/// <summary>
/// Profile for mapping between Cart entity and GetCartsResult
/// </summary>
public class GetCartsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetCarts operation
    /// </summary>
    public GetCartsProfile()
    {
        CreateMap<Cart, CartResult>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<CartItem, CartItemResult>();

        CreateMap<(IEnumerable<Cart> Carts, int TotalCount, int Page, int PageSize), GetCartsResult>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Carts))
            .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(src => src.TotalCount))
            .ForMember(dest => dest.CurrentPage, opt => opt.MapFrom(src => src.Page))
            .ForMember(dest => dest.TotalPages, opt => opt.MapFrom(src => (int)Math.Ceiling((double)src.TotalCount / src.PageSize)));
    }
}