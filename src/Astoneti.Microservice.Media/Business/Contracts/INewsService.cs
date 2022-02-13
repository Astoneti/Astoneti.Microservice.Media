using Astoneti.Microservice.Media.Business.Models;
using System.Collections.Generic;

namespace Astoneti.Microservice.Media.Business.Contracts
{
    public interface INewsService
    {
        List<NewsDto> GetList();

        NewsDto Get(int id);

        NewsDto Add(INewsAddDto item);

        NewsDto Edit(INewsEditDto item);

        bool Delete(int id);
    }
}
