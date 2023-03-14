namespace FirstAMLLibraryTest
{
    using FirstAMLLibrary;

    public class Step1Tests
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

        [Test]
        public void TestMultipleParcelsOutput()
        {
            var target = new Step1();
            var errorReporter = new ErrorReporter();
            Parcel[] parcels = { new Parcel("1", 10), new Parcel("2", 50), new Parcel("3", 100), new Parcel("4", 1) };
            var expected = "1\tMedium\t8\n2\tLarge\t15\n3\tXL\t25\n4\tSmall\t3\n======================\nTotal\t51\n";
            Assert.That(expected, Is.EqualTo(target.GetSendCostsWithTotal(errorReporter, parcels)));
        }
    }
}