namespace Astoneti.Microservice.Media.Business.Contracts
{
    public interface INewsAddDto
    {
        public int Id { get; }

        public string Title { get; }

        public string Body { get; }
    }
}
