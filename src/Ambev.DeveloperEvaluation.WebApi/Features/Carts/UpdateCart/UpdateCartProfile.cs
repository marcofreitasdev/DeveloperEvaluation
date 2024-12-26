using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

/// <summary>
/// Profile for mapping between Application and API UpdateCart responses
/// </summary>
public class UpdateCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateCart feature
    /// </summary>
    public UpdateCartProfile()
    {
        CreateMap<UpdateCartRequest, UpdateCartCommand>()
        .ForMember(dest => dest.Items, opt => opt.MapFrom(src =>
            src.Items.Select(item => new UpdateCartItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity
            }).ToList()));

        CreateMap<UpdateCartItemRequest, CartItem>();

        CreateMap<UpdateCartResult, UpdateCartResponse>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<UpdateCartItemResult, UpdateCartItemResponse>();
    }
}