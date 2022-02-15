using System;

namespace Astoneti.Microservice.Media.Business.Models
{
    public class CommentDto
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? NewsId { get; set; }

        public NewsDto News { get; set; }
    }
}
