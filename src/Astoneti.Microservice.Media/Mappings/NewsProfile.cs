using Astoneti.Microservice.Media.Business.Models;
using Astoneti.Microservice.Media.Models;
using AutoMapper;

namespace Astoneti.Microservice.Media.Mappings
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<NewsModel, NewsDto>().ReverseMap();
        }
    }
}
