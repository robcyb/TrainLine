namespace AddressProcessing.Tests.Contacts
{
    using AddressProcessing.Contacts;
    using NUnit.Framework;
    using System.IO;
    using Utils;

    [TestFixture]
    public class ContactsReaderTests
    {
        [Test]
        public void Should_use_a_streamreader_to_read_file()
        {
            Assert.Inconclusive();
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
