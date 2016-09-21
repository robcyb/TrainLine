namespace AddressProcessing.Contacts
{
    using System;
    using System.Threading.Tasks;

    class ContactsReader : IContactsReader
    {
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
