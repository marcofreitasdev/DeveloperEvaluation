using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

/// <summary>
/// Profile for mapping between Product entity and DeleteProductResult
/// </summary>
public class DeleteProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for DeleteProduct operation
    /// </summary>
    public DeleteProductProfile()
    {
        CreateMap<Product, DeleteProductResult>()
            .ForMember(dest => dest.Success, opt => opt.Ignore())
            .ForMember(dest => dest.Message, opt => opt.Ignore());
    }
}