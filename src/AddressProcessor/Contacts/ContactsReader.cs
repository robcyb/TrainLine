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
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool isEndOfStream()
        {
            throw new NotImplementedException();
        }

        public Task<Contacts> ReadContacts()
        {
            throw new NotImplementedException();
        }
    }
}
