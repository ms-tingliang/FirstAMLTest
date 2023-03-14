namespace FirstAMLLibraryTest
{
    using FirstAMLLibrary;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestSingleParcel0()
        {
            var target = new Step1();
            var errorReporter = new ErrorReporter();
            Assert.That(3.0, Is.EqualTo(target.GetSendCost(errorReporter, new Parcel(0))));
        }

        [Test]
        public void TestSingleParcel1()
        {
            var target = new Step1();
            var errorReporter = new ErrorReporter();
            Assert.That(3.0, Is.EqualTo(target.GetSendCost(errorReporter, new Parcel(1))));
        }

        [Test]
        public void TestSingleParcel10()
        {
            var target = new Step1();
            var errorReporter = new ErrorReporter();
            Assert.That(8.0, Is.EqualTo(target.GetSendCost(errorReporter, new Parcel(10))));
        }

        [Test]
        public void TestSingleParcel50()
        {
            var target = new Step1();
            var errorReporter = new ErrorReporter();
            Assert.That(15.0, Is.EqualTo(target.GetSendCost(errorReporter, new Parcel(50))));
        }

        [Test]
        public void TestSingleParcel100()
        {
            var target = new Step1();
            var errorReporter = new ErrorReporter();
            Assert.That(25.0, Is.EqualTo(target.GetSendCost(errorReporter, new Parcel(100))));
        }

        [Test]
        public void TestMultipleParcelsAscending()
        {
            var target = new Step1();
            var errorReporter = new ErrorReporter();
            Assert.That(51.0, Is.EqualTo(target.GetSendCost(errorReporter, new Parcel(1), new Parcel(10), new Parcel(50), new Parcel(100))));
        }

        [Test]
        public void TestMultipleParcelsDescending()
        {
            var target = new Step1();
            var errorReporter = new ErrorReporter();
            Assert.That(51.0, Is.EqualTo(target.GetSendCost(errorReporter, new Parcel(100), new Parcel(50), new Parcel(10), new Parcel(1))));
        }

        [Test]
        public void TestMultipleParcelsRandom()
        {
            var target = new Step1();
            var errorReporter = new ErrorReporter();
            Assert.That(51.0, Is.EqualTo(target.GetSendCost(errorReporter, new Parcel(10), new Parcel(50), new Parcel(100),  new Parcel(1))));
        }
    }
}