using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerLib
{
    interface IPassword
    {
        //Main password property
        public string password { get;  set; }

        //Methods
        public bool ComparePasswords(string psToCompare);
    }
}
