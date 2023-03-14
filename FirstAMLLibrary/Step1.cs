using System.Text;

namespace FirstAMLLibrary
{
    public class Step1
    {
        public string GetSendCostsWithTotal(ErrorReporter reporter, params Parcel[] parcels)
        {
            var classifier = new ParcelClassifier();
            var outputBuilder = new StringBuilder();
            var calculator = new ParcelCostCalculator();
            var totalCost = 0.0;
            foreach (var parcel in parcels)
            {
                var classification = classifier.GetClassification(parcel, reporter);
                var cost = calculator.CalculateUnitCost(parcel, classifier, reporter);
                outputBuilder.AppendFormat("{0}\t{1}\t{2}\n", parcel.Id, classification, cost);
                totalCost += cost;
            }
            outputBuilder.AppendFormat("======================\n");
            outputBuilder.AppendFormat("Total\t{0}\n", totalCost);
            return outputBuilder.ToString();
        }

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