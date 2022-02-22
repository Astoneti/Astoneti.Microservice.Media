using System;
namespace Astoneti.Service.NewsWorker
{
    public class News
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime PublicationDate { get; set; }
    }
}
