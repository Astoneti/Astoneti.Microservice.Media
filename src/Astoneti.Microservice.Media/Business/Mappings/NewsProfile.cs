using Astoneti.Microservice.Media.Business.Contracts;
using Astoneti.Microservice.Media.Business.Models;
using Astoneti.Microservice.Media.Data.Entities;
using AutoMapper;

namespace Astoneti.Microservice.Media.Business.Mappings
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<NewsEntity, NewsDto>().ReverseMap();

            CreateMap<NewsDto, NewsEntity>().ReverseMap();

            CreateMap<INewsAddDto, NewsEntity>().ReverseMap();

            CreateMap<INewsEditDto, NewsEntity>().ReverseMap();
        }
    }
}
