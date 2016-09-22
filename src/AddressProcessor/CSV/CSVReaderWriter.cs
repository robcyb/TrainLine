namespace AddressProcessing.CSV
{
    using Contacts;
    using System;
    using System.Threading;

    public class CSVReaderWriter : IDisposable
    {
        private IContactsReader contactsReader;
        private ICSVWriter csvWriter;

        // Exposing default constructor to ensure backwards compatibility.
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

        // For backwards compatability, keep existing method signature and delegate.
        public bool Read(string name, string postalAddress)
        {
            return Read(out name, out postalAddress);
        }

        public bool Read(out string name, out string postalAddress)
        {
            bool stillProcessing = !contactsReader.isEndOfStream();

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

            return stillProcessing;
        }

        public void Dispose()
        {
            if (contactsReader != null)
            {
                contactsReader.Dispose();
                contactsReader = null;
            }
            if (csvWriter != null)
            {
                csvWriter.Dispose();
                csvWriter = null;
            }

            GC.SuppressFinalize(this);
        }

        public void Close()
        {
            if (contactsReader != null)
            {
                contactsReader.Close();
            }
            if (csvWriter != null)
            {
                csvWriter.Close();
            }
        }
    }
}
