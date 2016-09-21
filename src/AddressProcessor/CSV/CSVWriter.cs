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
            var filesDirectory = Path.GetDirectoryName(outputFileName);

            if (!Directory.Exists(filesDirectory))
            {
                Directory.CreateDirectory(filesDirectory);
            }

            FileInfo fileInfo = new FileInfo(outputFileName);

            return fileInfo.CreateText();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(streamWriter != null);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter.Dispose();
                    streamWriter = null;
                }
            }
        }

        public Task WriteToCSV(params string[] columnCollection)
        {
            var newLine = string.Join(lineSeperator, columnCollection);
            return streamWriter.WriteLineAsync(newLine);
        }
    }
}