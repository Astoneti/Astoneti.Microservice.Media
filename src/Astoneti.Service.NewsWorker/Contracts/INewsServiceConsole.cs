using System.Threading.Tasks;

namespace Astoneti.Service.NewsWorker.Contracts
{
    public interface INewsServiceConsole
    {
        Task GetNewsListFromApi();

        Task PostNewsToApi();
    }
}
