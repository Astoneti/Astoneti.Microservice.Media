using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Astoneti.Service.NewsWorker
{
    public class Program
    {
        private static readonly HttpClient client = new();

        private static async Task<IList<News>> GetDataFromAPI()
        {
            return await client.GetFromJsonAsync<IList<News>>("https://localhost:5001/news");
        }

        private static async Task<IList<News>> PostDataFromConsoleToAPI()
        {
            return await client.PostAsJsonAsync<News>("https://localhost:5001/news");
        }

        public static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("API import starting...");

                var news = await GetDataFromAPI();

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
    }
}
