using Astoneti.Microservice.Media.Business.Models;
using Astoneti.Microservice.Media.Data.Entities;
using AutoMapper;

namespace Astoneti.Microservice.Media.Business.Mappings
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentEntity, CommentDto>().ReverseMap();

            CreateMap<CommentDto, CommentEntity>().ReverseMap();
        }
    }
}
    

