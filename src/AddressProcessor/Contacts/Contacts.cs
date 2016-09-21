namespace AddressProcessing.Contacts
{
    class Contacts
    {
        public Contacts(string name, string postalAddress)
        {
            Name = name;
            PostalAddress = postalAddress;
        }
        public string Name { get; set; }
        public string PostalAddress { get; set; }
    }
}
