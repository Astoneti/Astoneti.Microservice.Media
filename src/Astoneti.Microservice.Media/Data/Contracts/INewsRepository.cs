using Astoneti.Microservice.Media.Data.Entities;
using System.Collections.Generic;

namespace Astoneti.Microservice.Media.Data.Contracts
{
    public interface INewsRepository
    {
        List<NewsEntity> GetList();

        NewsEntity Get(int id);

        NewsEntity Insert(NewsEntity entity);

        NewsEntity Update(NewsEntity entity);

        bool Delete(int id);
    }
}
