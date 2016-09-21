namespace AddressProcessing.Contacts
{
    using System;
    using System.Threading.Tasks;

    interface IContactsReader : IDisposable
    {
        Task<Contacts> ReadContacts();

        void Close();

        bool isEndOfStream();
    }
}
