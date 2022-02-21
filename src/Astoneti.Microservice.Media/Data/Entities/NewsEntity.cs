using System;
using System.Collections.Generic;

namespace Astoneti.Microservice.Media.Data.Entities
{
    public class NewsEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime PublicationDate { get; set; }

        public List<CommentEntity> Comments { get; set; }
    }
}
