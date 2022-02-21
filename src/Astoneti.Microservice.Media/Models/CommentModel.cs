using System;

namespace Astoneti.Microservice.Media.Models
{
    public class CommentModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime CreatedDate { get; set; }

        public int NewsId { get; set; }
    }
}
