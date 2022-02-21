using Astoneti.Microservice.Media.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Astoneti.Microservice.Media.Data
{
    public class MediaDbContext : DbContext
    {
        public MediaDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }
        public DbSet<NewsEntity> News { get; set; }

        public DbSet<CommentEntity> Comments { get; set; }
    }
}
