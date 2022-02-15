using Astoneti.Microservice.Media.Business.Contracts;
using System;

namespace Astoneti.Microservice.Media.Models
{
    public class NewsPutModel : INewsEditDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime PublicationDate { get; set; }
    }
}
