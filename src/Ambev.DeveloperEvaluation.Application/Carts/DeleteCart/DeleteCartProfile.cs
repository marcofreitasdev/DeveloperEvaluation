using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

/// <summary>
/// Profile for mapping between Cart entity and DeleteCartResult
/// </summary>
public class DeleteCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for DeleteCart operation
    /// </summary>
    public DeleteCartProfile()
    {
        CreateMap<Cart, DeleteCartResult>()
            .ForMember(dest => dest.Success, opt => opt.Ignore())
            .ForMember(dest => dest.Message, opt => opt.Ignore());
    }
}