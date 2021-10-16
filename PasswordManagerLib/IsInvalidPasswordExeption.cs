using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerLib
{
    public class IsInvalidPasswordExeption : Exception
    {

        public IsInvalidPasswordExeption() : base() { }
        public IsInvalidPasswordExeption(string message) : base(message) { }

    }
}
