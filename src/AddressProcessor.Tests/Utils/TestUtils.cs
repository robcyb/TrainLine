namespace AddressProcessing.Tests.Utils
{
    using NUnit.Framework;
    using System.IO;

    public static class TestUtils
    {
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
