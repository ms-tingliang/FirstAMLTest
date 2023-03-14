namespace FirstAMLLibraryTest
{
    using FirstAMLLibrary;

    public class Step4Tests
    {
        [Test]
        public void TestMultipleParcelsOutput()
        {
            var target = new Step4();
            var errorReporter = new ErrorReporter();
            Parcel[] parcels = { new Parcel("1", 10, 2), new Parcel("2", 50, 7), new Parcel("3", 100, 3), new Parcel("4", 100, 100) };
            var calculator = new ParcelCostCalculator(true);
            var expectedNonSpeedy = "1\tMedium\t8\n2\tLarge\t17\n3\tXL\t25\n4\tXL\t100\n======================\nTotal\t150\n";
            Assert.That(expectedNonSpeedy, Is.EqualTo(target.GetSendCostsWithSpeedyAndWeightLimitSurchargeTotal(errorReporter, false, calculator, parcels)));
            var expectedSpeedy = "1\tMedium\t8\n2\tLarge\t17\n3\tXL\t25\n4\tXL\t100\nSpeedy\t\t150\n======================\nTotal\t300\n";
            Assert.That(expectedSpeedy, Is.EqualTo(target.GetSendCostsWithSpeedyAndWeightLimitSurchargeTotal(errorReporter, true, calculator, parcels)));
        }
    }
}