using System;

namespace Astoneti.Microservice.Media.Data.Entities
{
    public class CommentEntity
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime CreatedDate { get; set; }

        public int NewsId { get; set; }

        public NewsEntity News { get; set; }
    }
}
