using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    class ExitExeption : Exception
    {
        public ExitExeption() : base() { }
        public ExitExeption(string message) : base(message) { }
    }
}
