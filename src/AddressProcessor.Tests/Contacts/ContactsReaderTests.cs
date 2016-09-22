namespace AddressProcessing.Tests.Contacts
{
    using AddressProcessing.Contacts;
    using NUnit.Framework;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;
    using Utils;

    [TestFixture]
    public class ContactsReaderTests
    {
        [Test]
        public async Task Should_use_a_streamreader_to_read_file()
        {
            string contactName = "robert simmons";
            string contactPostalAddress = "Holborn, London";

            string newContact = string.Format(CultureInfo.InvariantCulture, "{0}\t{1}", contactName, contactPostalAddress);

            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var streamReader = new StreamReader(memoryStream))
            using (var contactsReader = new ContactsReader(streamReader, new char[] { '\t' }))
            {
                await streamWriter.WriteLineAsync(newContact);
                await streamWriter.FlushAsync();

                var contact = await contactsReader.ReadContacts();

                Assert.AreEqual(contactName, contact.Name);
                Assert.AreEqual(contactPostalAddress, contact.PostalAddress);
            }
        }

        [Test]
        public void Should_read_from_a_file()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void Should_throw_directorynotfoundexception_when_directory_not_exist()
        {
            string missingDirectory = @"unknown\notfound.csv";

            TestUtils.DeleteDirectory(missingDirectory);

            Assert.Throws<DirectoryNotFoundException>(delegate
            {
                var contactsReader = new ContactsReader(missingDirectory);
            });
        }

        [Test]
        public void Should_throw_filenotfoundexception_when_file_not_exist()
        {
            string missingFile = @"notfound.csv";

            TestUtils.DeleteFile(missingFile);

            Assert.Inconclusive();
        }

        [TestFixtureTearDown]
        public void TearDown()
        {

        }
    }
}
