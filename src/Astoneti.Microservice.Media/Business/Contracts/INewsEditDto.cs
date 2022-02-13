using System;

namespace Astoneti.Microservice.Media.Business.Contracts
{
    public interface INewsEditDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime PublicationDate { get; set; }
    }
}
