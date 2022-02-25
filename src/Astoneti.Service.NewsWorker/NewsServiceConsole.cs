using Astoneti.Service.NewsWorker.Contracts;
using Astoneti.Service.NewsWorker.Models;
using System;
using System.Threading.Tasks;

namespace Astoneti.Service.NewsWorker
{
    public class NewsServiceConsole : INewsServiceConsole
    {
        private readonly INewsProviderConsole _newsProviderConsole;

        public NewsServiceConsole(INewsProviderConsole newsProviderConsole)
        {
            _newsProviderConsole = newsProviderConsole;
        }

        public async Task GetNewsListFromApi()
        {
            try
            {
                Console.WriteLine("API import starting...");

                var news = await _newsProviderConsole.GetNewsListFromApi();

                foreach (var n in news)
                    Console.WriteLine(
                       $"News List : { n.Id }, " +
                       $"{ n.Title }, " +
                       $"{ n.Body }, " +
                       $"{ n.PublicationDate }"
                 );

                Console.WriteLine(
                    "Total News is : "
                    + news.Count
                 );

                Console.WriteLine(
                    "API import ended successfully"
                 );
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Error : "
                    + ex.Message
                 );
            }
        }

        public async Task PostNewsToApi()
        {
            Console.WriteLine("API import starting...");

            Console.WriteLine("Type Title : ");
            var title = Console.ReadLine();

            Console.WriteLine("Type Body : ");
            var body = Console.ReadLine();

            var newsPostModel = new News
            {
                Title = title,
                Body = body
            };

            await _newsProviderConsole.PostNewsToApi(newsPostModel);

            Console.WriteLine(
                $"Posting item returns : " +
                $"Title : {newsPostModel.Title}, " +
                $"Body : {newsPostModel.Body}, " +
                $"PublicationDate : {newsPostModel.PublicationDate}"
            );

            Console.WriteLine("API Post ended successfully");
        }
    }
}
