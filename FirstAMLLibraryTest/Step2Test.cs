namespace FirstAMLLibraryTest
{
    using FirstAMLLibrary;

    public class Step2Tests
    {
        [Test]
        public void TestMultipleParcelsOutput()
        {
            var target = new Step2();
            var errorReporter = new ErrorReporter();
            Parcel[] parcels = { new Parcel("1", 10), new Parcel("2", 50), new Parcel("3", 100), new Parcel("4", 1) };
            var expectedNonSpeedy = "1\tMedium\t8\n2\tLarge\t15\n3\tXL\t25\n4\tSmall\t3\n======================\nTotal\t51\n";
            Assert.That(expectedNonSpeedy, Is.EqualTo(target.GetSendCostsWithSpeedyTotal(errorReporter, false, parcels)));
            var expectedSpeedy = "1\tMedium\t8\n2\tLarge\t15\n3\tXL\t25\n4\tSmall\t3\nSpeedy\t\t51\n======================\nTotal\t102\n";
            Assert.That(expectedSpeedy, Is.EqualTo(target.GetSendCostsWithSpeedyTotal(errorReporter, true, parcels)));
        }
    }
}