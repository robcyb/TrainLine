﻿namespace AddressProcessing.Contacts
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class ContactsReader : IContactsReader
    {
        private StreamReader streamReader = null;
        private char[] tabSeperator;
        private readonly int firstColumn = 0;
        private readonly int secondColumn = 1;

        public ContactsReader(string fileName)
            : this(File.OpenText(fileName), new char[] { '\t' })
        {
        }

        public ContactsReader(StreamReader streamReader, char[] tabSeperator)
        {
            this.streamReader = streamReader;
            this.tabSeperator = tabSeperator;
        }

        public void Close()
        {
            streamReader.Close();
        }

        public void Dispose()
        {
            Dispose(streamReader != null);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                    streamReader.Dispose();
                    streamReader = null;
                }
            }
        }

        public bool isEndOfStream()
        {
            return streamReader.EndOfStream;
        }

        public async Task<Contacts> ReadContacts()
        {
            string line = string.Empty;
            string[] columns = null;

            line = await streamReader.ReadLineAsync();

            if (!string.IsNullOrEmpty(line))
            {
                columns = line.Split('\t');
            }

            // We should have minimum of 2 columns from the CSV file.
            if (columns == null || columns.Length < 2)
            {
                return null;
            }

            // Column 0 contains the name, column 1 contains the postal address.
            return new Contacts(columns[firstColumn], columns[secondColumn]);
        }
    }
}
