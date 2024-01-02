using api.DTOs.Comment;
using api.DTOs.Request;
using api.DTOs.Stock;
using api.Models;
using AutoMapper;

namespace api.Mappers
{
    public class AppMapperProfile : Profile
    {
        public AppMapperProfile() 
        { 
            CreateMap<StockDto, Stock>().ReverseMap();
            CreateMap<Stock, CreateStockRequest>().ReverseMap();
            CreateMap<Stock, UpdateStockRequest>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<Comment, CreateCommentRequest>().ReverseMap();
            CreateMap<Comment, UpdateCommentRequest>().ReverseMap();
        }
    }
}
