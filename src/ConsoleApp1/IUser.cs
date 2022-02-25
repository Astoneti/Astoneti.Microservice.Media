using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public interface IUser
    {
        string Name { get; set; }

        void TruncateName(string name);
    }
}
