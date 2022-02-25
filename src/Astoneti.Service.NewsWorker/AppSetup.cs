using Astoneti.Service.NewsWorker.Contracts;
using System.Threading.Tasks;

namespace Astoneti.Service.NewsWorker
{
    public class AppSetup
    {
        private readonly INewsServiceConsole _newsServiceConsole;

        public AppSetup(INewsServiceConsole newsServiceConsole)
        {
            _newsServiceConsole = newsServiceConsole;
        }

        public async Task RunAsync()
        {
            await _newsServiceConsole.GetNewsListFromApi();

            await _newsServiceConsole.PostNewsToApi();
        }
    }
}
