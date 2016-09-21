namespace AddressProcessing.CSV
{
    using System;
    using System.Threading.Tasks;

    public interface ICSVWriter : IDisposable
    {
        Task WriteToCSV(params string[] columnCollection);

        void Close();
    }
}
