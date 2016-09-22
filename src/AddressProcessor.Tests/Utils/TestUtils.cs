namespace AddressProcessing.Tests.Utils
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.IO;
    using AddressProcessing.Contacts;
    using System.Threading.Tasks;
    using System;

    public static class TestUtils
    {
        private const int FirstColumnIndex = 0;
        private const int SecondColumnIndex = 1;

        public static async Task<IEnumerable<Contacts>> ReadFromFile(string file)
        {
            var listOfContacts = new List<Contacts>();

            using (var contactsReader = new ContactsReader(file))
            {
                while (!contactsReader.isEndOfStream())
                {
                    var foundContact = await contactsReader.ReadContacts();
                    if (foundContact != null)
                    {
                        listOfContacts.Add(foundContact);
                    }
                }
            }

            return listOfContacts;
        }

        public static Contacts ContactFromLine(string line)
        {
            char[] tab = { '\t' };

            var lineContact = line.Split(tab, StringSplitOptions.RemoveEmptyEntries);

            if (lineContact.Length >= 2)
            {
                return new Contacts(lineContact[FirstColumnIndex], lineContact[SecondColumnIndex]);
            }

            return null;
        }

        /// <summary>
        /// DeleteDirectory is a helper method used within the tests to delete a directory, and all files/folders under it.
        /// </summary>
        /// <param name="directory">The directory to delete</param>
        public static void DeleteDirectory(string directory)
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
        public static void DeleteFile(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }

            Assert.False(File.Exists(file));
        }
    }
}
