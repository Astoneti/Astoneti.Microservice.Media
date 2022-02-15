using Astoneti.Microservice.Media.Data.Contracts;
using Astoneti.Microservice.Media.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Astoneti.Microservice.Media.Data
{
    public class NewsRepository : INewsRepository
    {
        public NewsRepository(MediaDbContext dbContext)
        {
            AppContext = dbContext;
        }

        protected DbContext AppContext { get; }

        public List<NewsEntity> GetList()
        {
            return AppContext.Set<NewsEntity>().ToList();
        }

        public NewsEntity Get(int id)
        {
            return AppContext.Set<NewsEntity>().FirstOrDefault(x => x.Id == id);
        }

        public NewsEntity Insert(NewsEntity entity)
        {
            AppContext.Set<NewsEntity>().Add(entity);

            AppContext.SaveChanges();

            return entity;
        }

        public NewsEntity Update(NewsEntity entity)
        {
            AppContext.Set<NewsEntity>().Update(entity);

            AppContext.SaveChanges();

            return entity;
        }

        public bool Delete(int id)
        {
            var entity = AppContext.Set<NewsEntity>().FirstOrDefault(_ => _.Id == id);

            if (entity == null)
            {
                return false;
            }

            AppContext.Set<NewsEntity>().Remove(entity);

            var result = AppContext.SaveChanges();

            return result == 1;
        }
    }
}
