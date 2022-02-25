using Astoneti.Service.NewsWorker.Contracts;
using Astoneti.Service.NewsWorker.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Astoneti.Service.NewsWorker
{
    public class NewsProviderConsole : INewsProviderConsole
    {
        private readonly HttpClient _client = new();

        public async Task<List<News>> GetNewsListFromApi()
        {
            return await _client.GetFromJsonAsync<List<News>>("https://localhost:5001/news");
        }

        public async Task PostNewsToApi(News item)
        {
            await _client.PostAsJsonAsync("https://localhost:5001/news", item);
        }
    }
}
