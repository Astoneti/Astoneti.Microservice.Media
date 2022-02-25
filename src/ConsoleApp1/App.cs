using Microsoft.Extensions.Configuration;
using System;

namespace ConsoleApp1
{
    public class App
    {
        private readonly IConfiguration _config;

        //#1
        private readonly IUser _user;

        public App(IConfiguration config, 
            //#1 TODO
            IUser user
            )
        {
            _config = config;
            //#1
            _user = user;
        }

        public void Run()
        {
            //
            _user.TruncateName("Jerry     ");
            Console.WriteLine($"Hello {_user.Name}");


            //_newsService.GetNewsListFromApi();
        }
    }
}
