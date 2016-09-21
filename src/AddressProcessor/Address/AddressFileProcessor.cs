namespace AddressProcessing.Address
{
    using System;
    using v1;
    using CSV;

    public class AddressFileProcessor
    {
        private readonly IMailShot _mailShot;

        public AddressFileProcessor(IMailShot mailShot)
        {
            if (mailShot == null) throw new ArgumentNullException("mailShot");
            _mailShot = mailShot;
        }

        public void Process(string inputFile)
        {
            using (var reader = new CSVReaderWriter())
            {
                reader.Open(inputFile, CSVReaderWriter.Mode.Read);

                string column1, column2;

                while (reader.Read(out column1, out column2))
                {
                    _mailShot.SendMailShot(column1, column2);
                }

                reader.Close();
            }
        }
    }
}
