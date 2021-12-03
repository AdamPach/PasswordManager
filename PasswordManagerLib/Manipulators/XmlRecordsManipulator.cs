using PasswordManagerLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace PasswordManagerLib.Manipulators
{
    public class XmlRecordsManipulator : IRecordsManipulator
    {
        public XmlSerializer serializer { private get; init; }
        private XmlWriterSettings writerSettings;

        public XmlRecordsManipulator()
        {
            serializer = new XmlSerializer(typeof(List<Record>));
            writerSettings = new XmlWriterSettings();
            writerSettings.Indent = true;
        }

        public async Task<IEnumerable<Record>> ReadRecords(string fileName)
        {
            using (XmlReader reader = XmlReader.Create(fileName))
            {
                List<Record> accounts;
                accounts = await Task.Run<List<Record>>(() => (List<Record>)serializer.Deserialize(reader));
                return accounts;
            }
        }

        public async Task<bool> WriteRecords(IEnumerable<Record> records, string fileName)
        {
            using(var writer = XmlTextWriter.Create(fileName, writerSettings))
            {
                await Task.Run(() => serializer.Serialize(writer, (List<Record>)records));
            }

            return true;
        }
    }
}
