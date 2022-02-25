using Astoneti.Service.NewsWorker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Astoneti.Service.NewsWorker.Contracts
{
    public interface INewsProviderConsole
    {
        Task<List<News>> GetNewsListFromApi();

        Task PostNewsToApi(News item);
    }
}
