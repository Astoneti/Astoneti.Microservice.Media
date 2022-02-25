using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class User : IUser
    {
        public string Name { get; set; }

        public void TruncateName(string name)
        {
            Name = name.Trim();
        }
    }
}
