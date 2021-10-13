using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerLib
{
    public class NotLoggedExeption : Exception
    {
        public NotLoggedExeption(string message):base(message)
        {

        }

    }
}
