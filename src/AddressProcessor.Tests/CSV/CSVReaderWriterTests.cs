namespace Csv.Tests
{
    using NUnit.Framework;
    using System.IO;
    using AddressProcessing.CSV;
    using System;

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
        }

        [Test]
        public void Should_throw_when_reading_malformed_file()
        {
            // As per code review - we know (currently) that if there is only 1 column we will get an IndexOutOfRangeException.
            // Let's test that explicitly.

            string pathToMalformedFile = "test_data/contacts-malformed.csv";

            var csvReaderWriter = new CSVReaderWriter();

            csvReaderWriter.Open(pathToMalformedFile, CSVReaderWriter.Mode.Read);

            string firstColumn, secondColumn;

            Assert.Throws<IndexOutOfRangeException>(delegate
            {
                csvReaderWriter.Read(out firstColumn, out secondColumn);
            });
        }

        [Test]
        public void Should_parse_valid_CSV_file_correctly()
        {
            string pathToCSV = "test_data/contacts.csv";

            var csvReaderWriter = new CSVReaderWriter();

            csvReaderWriter.Open(pathToCSV, CSVReaderWriter.Mode.Read);

            string firstColumn, secondColumn;

            var csvOutput = csvReaderWriter.Read(out firstColumn, out secondColumn);

            Assert.True(csvOutput);
        }

        [Theory]
        public void Should_throw_when_invalid_or_missing_directory(CSVReaderWriter.Mode mode)
        {
            string missingFileAndDirectory = @"input\notfound.csv";

            DeleteDirectory(missingFileAndDirectory);

            var csvReaderWriter = new CSVReaderWriter();

            Assert.Throws<DirectoryNotFoundException>(delegate
            {
                csvReaderWriter.Open(missingFileAndDirectory, mode);
            });
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
