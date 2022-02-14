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
            DbContext = dbContext;
        }

        protected DbContext DbContext { get; }

        public List<NewsEntity> GetList()
        {
            return DbContext.Set<NewsEntity>().ToList();
        }

        public NewsEntity Get(int id)
        {
            return DbContext.Set<NewsEntity>().FirstOrDefault(x => x.Id == id);
        }

        public NewsEntity Insert(NewsEntity entity)
        {
            DbContext.Set<NewsEntity>().Add(entity);

            DbContext.SaveChanges();

            return entity;
        }

        public NewsEntity Update(NewsEntity entity)
        {
            DbContext.Set<NewsEntity>().Update(entity);

            DbContext.SaveChanges();

            return entity;
        }

        public bool Delete(int id)
        {
            var entity = DbContext.Set<NewsEntity>().FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                return false;
            }

            DbContext.Set<NewsEntity>().Remove(entity);

            var result = DbContext.SaveChanges();

            return result == 1;
        }
    }
}
