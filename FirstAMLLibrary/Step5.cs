using System.Text;

namespace FirstAMLLibrary
{
    public class Step5
    {
        public string GetDiscountedSendCostTotal(ErrorReporter reporter, bool speedyOption, ParcelDiscounter discounter, ParcelCostCalculator calculator, params Parcel[] parcels)
        {
            var classifier = new ParcelClassifier();
            discounter.CalculateDiscount(classifier, calculator, reporter, parcels);

            var outputBuilder = new StringBuilder();
            var totalCost = 0.0;
            foreach (var parcel in parcels)
            {
                var classification = classifier.GetClassification(parcel, reporter);
                var cost = calculator.CalculateUnitCost(parcel, classifier, reporter);
                outputBuilder.AppendFormat("{0}\t{1}\t{2}\n", parcel.Id, classification, cost);
                var discount = discounter.GetDiscount(parcel);
                if (!double.IsNaN(discount))
                {
                    outputBuilder.AppendFormat("{0}\tDiscount\t{1}\n", parcel.Id, -discount);
                    cost -= discount;
                }
                totalCost += cost;
            }

            if (speedyOption)
            {
                outputBuilder.AppendFormat("Speedy\t\t{0}\n", totalCost);
                totalCost += totalCost;
            }
            outputBuilder.AppendFormat("======================\n");
            outputBuilder.AppendFormat("Total\t{0}\n", totalCost);
            return outputBuilder.ToString();
        }
    }
}
