using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerLib.Models
{
    public class Record
    {
        public string ServiceName { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public Record()
        {

        }

        public Record(string Name, string Password, string ServiceName, string Url)
        {
            this.Name = Name;
            this.Password = Password;
            this.ServiceName = ServiceName;
            this.Url = Url;
        }
    }
}
