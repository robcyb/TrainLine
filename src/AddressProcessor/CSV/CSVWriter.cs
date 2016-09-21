namespace AddressProcessing.CSV
{
    using System;
    using System.Threading.Tasks;

    class CSVWriter : ICSVWriter
    {
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
