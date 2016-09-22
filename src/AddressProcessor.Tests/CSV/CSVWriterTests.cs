namespace AddressProcessing.Tests.CSV
{
    using AddressProcessing.CSV;
    using NUnit.Framework;
    using System.Globalization;
    using System.IO;
    using Utils;

    [TestFixture]
    public class CSVWriterTests
    {
        private const string TestDataOutputDirectory = "output";
        private const string NewLineSeperator = "\n";

        [Test]
        public void Should_write_new_file_when_not_exist()
        {
            string fileNotExist = @"output\notexist.csv";

            TestUtils.DeleteFile(fileNotExist);

            using (new CSVWriter(fileNotExist, NewLineSeperator))
            {
                Assert.True(File.Exists(fileNotExist));
            }
        }

        [Test]
        public void Should_create_directory_when_not_exist()
        {
            string directory = "notexists";
            string directoryNotExist = string.Format(CultureInfo.InvariantCulture, @"{0}\notfound.csv", directory);

            TestUtils.DeleteDirectory(directoryNotExist); // make sure it definately doesn't exist!

            using (new CSVWriter(directoryNotExist, NewLineSeperator))
            {
                Assert.True(Directory.Exists(directory));
            }
        }

        [Test]
        public void Should_write_to_file_correctly()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void Should_use_a_streamwriter_to_write_file()
        {
            Assert.Inconclusive();
        }

        [TestFixtureTearDown]
        public void TearDown()
        {

        }
    }
}
