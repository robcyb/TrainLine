namespace Csv.Tests
{
    using AddressProcessing.CSV;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Globalization;
    using System.IO;

    [TestFixture]
    public class CSVReaderWriterTests
    {
        private const string TestDataOutputDirectory = "output";

        [TestFixtureSetUp]
        public void Setup()
        {
            if (!Directory.Exists(TestDataOutputDirectory))
            {
                Directory.CreateDirectory(TestDataOutputDirectory);
            }
        }

        [Test]
        public void Should_throw_with_not_found_file_with_read_mode()
        {
            string notFoundFile = @"notfound.csv";

            var csvReaderWriter = new CSVReaderWriter();

            Assert.Throws<FileNotFoundException>(delegate
            {
                csvReaderWriter.Open(notFoundFile, CSVReaderWriter.Mode.Read);
            });
        }

        [Test]
        public void Should_create_file_for_missing_file_with_write_mode()
        {
            var missingFileToCreate = Path.Combine(TestDataOutputDirectory, "new.csv");

            DeleteFile(missingFileToCreate); // make sure it definately doesn't exist!

            var csvReaderWriter = new CSVReaderWriter();
            csvReaderWriter.Open(missingFileToCreate, CSVReaderWriter.Mode.Write);

            Assert.True(File.Exists(missingFileToCreate));
        }

        [Test]
        public void Should_return_false_when_reading_empty_file()
        {
            string pathToEmptyFile = "test_data/contacts-empty.csv";

            var csvReaderWriter = new CSVReaderWriter();

            csvReaderWriter.Open(pathToEmptyFile, CSVReaderWriter.Mode.Read);

            string firstColumn, secondColumn;

            var csvOutput = csvReaderWriter.Read(out firstColumn, out secondColumn);

            Assert.False(csvOutput);

            Assert.IsNullOrEmpty(firstColumn);
            Assert.IsNullOrEmpty(secondColumn);
        }

        [Test]
        public void Should_handle_reading_malformed_file()
        {
            string pathToMalformedFile = "test_data/contacts-malformed.csv";

            var csvReaderWriter = new CSVReaderWriter();

            csvReaderWriter.Open(pathToMalformedFile, CSVReaderWriter.Mode.Read);

            string name, postalAddress;

            Assert.IsTrue(csvReaderWriter.Read(out name, out postalAddress));

            Assert.IsNullOrEmpty(name);
            Assert.IsNullOrEmpty(postalAddress);
        }

        [Test]
        public void Should_parse_valid_CSV_file_correctly()
        {
            string pathToCSV = "test_data/contacts.csv";

            var csvReaderWriter = new CSVReaderWriter();

            csvReaderWriter.Open(pathToCSV, CSVReaderWriter.Mode.Read);

            string name, postalAddress;

            var csvOutput = csvReaderWriter.Read(out name, out postalAddress);

            Assert.IsNotNullOrEmpty(name);
            Assert.IsNotNullOrEmpty(postalAddress);
        }

        [Test]
        public void Should_throw_with_unknown_file_mode_message_when_invalid_mode()
        {
            string testFile = @"test_data/contacts-empty.csv";
            var invalidMode = (CSVReaderWriter.Mode)3;
            string expectedException = string.Format(CultureInfo.InvariantCulture, "Unknown file mode for {0}", testFile);

            var csvReaderWriter = new CSVReaderWriter();

            var exception = Assert.Throws<Exception>(delegate
            {
                csvReaderWriter.Open(testFile, invalidMode);
            });

            Assert.AreEqual(expectedException, exception.Message);
        }

        [Test]
        public void Should_throw_when_invalid_or_missing_directory_on_read()
        {
            string missingFileAndDirectory = @"input\notfound.csv";

            DeleteDirectory(missingFileAndDirectory);

            var csvReaderWriter = new CSVReaderWriter();

            Assert.Throws<DirectoryNotFoundException>(delegate
            {
                csvReaderWriter.Open(missingFileAndDirectory, CSVReaderWriter.Mode.Read);
            });
        }


        [TestFixtureTearDown]
        public void TearDown()
        {
            if (Directory.Exists(TestDataOutputDirectory))
            {
                Directory.Delete(TestDataOutputDirectory, true);
            }
        }

        /// <summary>
        /// DeleteDirectory is a helper method used within the tests to delete a directory, and all files/folders under it.
        /// </summary>
        /// <param name="directory">The directory to delete</param>
        private void DeleteDirectory(string directory)
        {
            var targetDirectory = Path.GetDirectoryName(directory);

            if (Directory.Exists(targetDirectory))
            {
                Directory.Delete(targetDirectory, true); // delete the directory, and all files/folders under it.
            }

            Assert.False(Directory.Exists(targetDirectory));
        }

        /// <summary>
        /// DeleteFile is a helper method used within the tests to delete a file under a given directory.
        /// </summary>
        /// <param name="file">The full path to the file to delete</param>
        private void DeleteFile(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }

            Assert.False(File.Exists(file));
        }
    }
}
