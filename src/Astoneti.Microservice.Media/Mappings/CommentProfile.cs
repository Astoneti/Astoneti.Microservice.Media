using Astoneti.Microservice.Media.Business.Models;
using Astoneti.Microservice.Media.Models;
using AutoMapper;

namespace Astoneti.Microservice.Media.Mappings
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentModel, CommentDto>().ReverseMap();
        }
    }
}
