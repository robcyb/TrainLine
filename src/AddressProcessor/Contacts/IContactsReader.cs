namespace AddressProcessing.Contacts
{
    using System;
    using System.Threading.Tasks;

    public interface IContactsReader : IDisposable
    {
        Task<Contacts> ReadContacts();

        void Close();

        bool isEndOfStream();
    }
}
