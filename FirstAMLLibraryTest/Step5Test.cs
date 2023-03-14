namespace FirstAMLLibraryTest
{
    using FirstAMLLibrary;

    public class Step5Tests
    {
        [Test]
        public void TestMultipleParcelsOutput()
        {
            var target = new Step5();
            var errorReporter = new ErrorReporter();
            var calculator = new ParcelCostCalculator(true);
            var discounter = new ParcelDiscounter();
            
            Parcel[] parcels = { new Parcel("1", 10, 2), new Parcel("2", 10, 2), new Parcel("3", 10, 2), new Parcel("4", 10, 4), new Parcel("5", 10, 4), new Parcel("6", 10, 4) };
            var expectedNonSpeedy = "1\tMedium\t8\n1\tDiscount\t-8\n2\tMedium\t8\n3\tMedium\t8\n4\tMedium\t10\n4\tDiscount\t-10\n5\tMedium\t10\n6\tMedium\t10\n======================\nTotal\t36\n";
            Assert.That(expectedNonSpeedy, Is.EqualTo(target.GetDiscountedSendCostTotal(errorReporter, false, discounter, calculator, parcels)));
            var expectedSpeedy = "1\tMedium\t8\n1\tDiscount\t-8\n2\tMedium\t8\n3\tMedium\t8\n4\tMedium\t10\n4\tDiscount\t-10\n5\tMedium\t10\n6\tMedium\t10\nSpeedy\t\t36\n======================\nTotal\t72\n";
            Assert.That(expectedSpeedy, Is.EqualTo(target.GetDiscountedSendCostTotal(errorReporter, true, discounter, calculator, parcels)));
        }
    }
}