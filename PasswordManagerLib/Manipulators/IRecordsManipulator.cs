using PasswordManagerLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerLib.Manipulators
{
    public interface IRecordsManipulator
    {
        public Task<bool> WriteRecords(IEnumerable<Record> records, string fileName);
        public Task<IEnumerable<Record>> ReadRecords(string fileName);
    }
}
