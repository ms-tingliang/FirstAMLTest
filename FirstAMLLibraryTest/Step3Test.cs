namespace FirstAMLLibraryTest
{
    using FirstAMLLibrary;

    public class Step3Tests
    {
        [Test]
        public void TestMultipleParcelsOutput()
        {
            var target = new Step3();
            var errorReporter = new ErrorReporter();
            Parcel[] parcels = { new Parcel("1", 10, 2), new Parcel("2", 50, 7), new Parcel("3", 100, 3), new Parcel("4", 100, 100) };
            var expectedNonSpeedy = "1\tMedium\t8\n2\tLarge\t17\n3\tXL\t25\n4\tXL\t205\n======================\nTotal\t255\n";
            Assert.That(expectedNonSpeedy, Is.EqualTo(target.GetSendCostsWithSpeedyAndWeightLimitSurchargeTotal(errorReporter, false, parcels)));
            var expectedSpeedy = "1\tMedium\t8\n2\tLarge\t17\n3\tXL\t25\n4\tXL\t205\nSpeedy\t\t255\n======================\nTotal\t510\n";
            Assert.That(expectedSpeedy, Is.EqualTo(target.GetSendCostsWithSpeedyAndWeightLimitSurchargeTotal(errorReporter, true, parcels)));
        }
    }
}