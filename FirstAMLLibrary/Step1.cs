namespace FirstAMLLibrary
{
    public class Step1
    {
        public double GetSendCost(ErrorReporter reporter, params Parcel[] parcels)
        {
            double totalCost = 0;
            var classifier = new ParcelClassifier();
            var calculator = new ParcelCostCalculator();
            foreach (var parcel in parcels)
            {
                totalCost += calculator.CalculateUnitCost(parcel, classifier, reporter);
            }
            return totalCost;
        }
    }
}