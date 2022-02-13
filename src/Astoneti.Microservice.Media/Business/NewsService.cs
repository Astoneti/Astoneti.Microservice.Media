using Astoneti.Microservice.Media.Business.Contracts;
using Astoneti.Microservice.Media.Business.Models;
using Astoneti.Microservice.Media.Data.Entities;
using AutoMapper;
using System.Collections.Generic;

namespace Astoneti.Microservice.Media.Business
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly IMapper _mapper;

        public NewsService(INewsRepository ownerRepository, IMapper mapper)
        {
            _newsRepository = neswsRepository;
            _mapper = mapper;
        }

        public List<NewsDto> GetList()
        {
            var items = _newsRepository.GetList();

            return _mapper.Map<List<NewsDto>>(
                items
            );
        }

        public NewsDto Get(int id)
        {
            var item = _newsRepository.Get(id);

            return _mapper.Map<NewsDto>(
                item
            );
        }

        public NewsDto Add(INewsAddDto item)
        {
            var entity = _mapper.Map<NewsEntity>(item);

            _newsRepository.Insert(entity);

            return _mapper.Map<NewsDto>(entity);
        }

        public NewsDto Edit(INewsEditDto item)
        {
            var entity = _newsRepository.Get(item.Id);

            if (entity == null)
            {
                return null;
            }

            _mapper.Map(item, entity);

            _newsRepository.Update(entity);

            return _mapper.Map<NewsDto>(entity);
        }

        public bool Delete(int id)
        {
            return _newsRepository.Delete(id);
        }
    }
}
