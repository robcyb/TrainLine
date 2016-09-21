namespace AddressProcessing.Contacts
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    class ContactsReader : IContactsReader
    {
        private StreamReader streamReader = null;
        private char[] tabSeperator;

        public ContactsReader(string fileName)
            : this(File.OpenText(fileName), new char[] { '\t' })
        {

        }

        public ContactsReader(StreamReader streamReader, char[] tabSeperator)
        {
            this.streamReader = streamReader;
            this.tabSeperator = tabSeperator;
        }

        public void Close()
        {
            streamReader.Close();
        }

        public void Dispose()
        {
            Dispose(streamReader != null);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                    streamReader.Dispose();
                    streamReader = null;
                }
            }
        }

        public bool isEndOfStream()
        {
            return streamReader.EndOfStream;
        }

        public Task<Contacts> ReadContacts()
        {
            throw new NotImplementedException();
        }
    }
}
