using System;

namespace Astoneti.Microservice.Media.Business.Models
{
    public class NewsDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime PublicationDate { get; set; }
    }
}
