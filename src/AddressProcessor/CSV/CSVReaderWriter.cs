namespace AddressProcessing.CSV
{
    using Contacts;
    using System;
    using System.IO;
    using System.Threading;

    /*
        2) Refactor this class into clean, elegant, rock-solid & well performing code, without over-engineering.
           Assume this code is in production and backwards compatibility must be maintained.
    */

    public class CSVReaderWriter : IDisposable
    {
        private IContactsReader contactsReader = null;
        private ICSVWriter csvWriter;

        private StreamReader _readerStream = null;
        private StreamWriter _writerStream = null;

        public CSVReaderWriter()
        {
        }

        public CSVReaderWriter(IContactsReader contactsReader, ICSVWriter csvWriter)
        {
            this.contactsReader = contactsReader;
            this.csvWriter = csvWriter;
        }

        [Flags]
        public enum Mode { Read = 1, Write = 2 };

        public void Open(string fileName, Mode mode)
        {
            if (mode == Mode.Read)
            {
                contactsReader = new ContactsReader(fileName);
            }
            else if (mode == Mode.Write)
            {
                csvWriter = new CSVWriter(fileName, "\t");
            }
            else
            {
                throw new Exception("Unknown file mode for " + fileName);
            }
        }

        public void Write(params string[] columns)
        {
            csvWriter.WriteToCSV(columns).Wait(Timeout.Infinite);
        }

        public bool Read(string name, string postalAddress)
        {
            return Read(out name, out postalAddress);
        }

        public bool Read(out string name, out string postalAddress)
        {
            bool done = !contactsReader.isEndOfStream();

            var contact = contactsReader.ReadContacts().Result;

            if (contact == null)
            {
                name = null;
                postalAddress = null;
            }
            else
            {
                name = contact.Name;
                postalAddress = contact.PostalAddress;
            }

            return done;
        }

        private void WriteLine(string line)
        {
            _writerStream.WriteLine(line);
        }

        private string ReadLine()
        {
            return _readerStream.ReadLine();
        }

        public void Close()
        {
            if (_writerStream != null)
            {
                _writerStream.Close();
            }

            if (_readerStream != null)
            {
                _readerStream.Close();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
