namespace AddressProcessing.CSV
{
    using System;
    using System.Threading.Tasks;
    using System.IO;

    public class CSVWriter : ICSVWriter
    {
        private string lineSeperator = string.Empty;
        private StreamWriter streamWriter = null;

        public CSVWriter(string outputFileName, string seperator)
            : this(CreateWriteStream(outputFileName), seperator)
        {
        }

        public CSVWriter(StreamWriter streamWriter, string seperator)
        {
            this.streamWriter = streamWriter;
            lineSeperator = seperator;
        }

        private static StreamWriter CreateWriteStream(string outputFileName)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task WriteToCSV(params string[] columnCollection)
        {
            throw new NotImplementedException();
        }
    }
}
