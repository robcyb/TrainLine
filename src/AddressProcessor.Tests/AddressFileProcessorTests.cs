namespace AddressProcessing.Tests
{
    using Address;
    using Address.v1;
    using NUnit.Framework;

    [TestFixture]
    public class AddressFileProcessorTests
    {
        private FakeMailShotService fakeMailShotService;
        private const string TestInputFile = @"test_data\contacts.csv";

        [SetUp]
        public void SetUp()
        {
            fakeMailShotService = new FakeMailShotService();
        }

        [Test]
        public void Should_send_mail_using_mailshot_service()
        {
            var processor = new AddressFileProcessor(fakeMailShotService);
            processor.Process(TestInputFile);

            Assert.That(fakeMailShotService.Counter, Is.EqualTo(229)); // TODO: possible issue relying on this hard-coded value.
        }

        internal class FakeMailShotService : IMailShot
        {
            internal int Counter { get; private set; }

            public void SendMailShot(string name, string address)
            {
                Counter++;
            }
        }
    }
}