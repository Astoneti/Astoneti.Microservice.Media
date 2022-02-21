 using Astoneti.Microservice.Media.Data.Contracts;
using Astoneti.Microservice.Media.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Astoneti.Microservice.Media.Data
{
    public class CommentRepository : ICommentRepository
    {
        public CommentRepository(MediaDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected DbContext DbContext { get; }

        public List<CommentEntity> GetList()
        {
            return DbContext.Set<CommentEntity>().ToList();
        }
    }
}
