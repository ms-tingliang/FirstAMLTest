﻿using System.Text;

namespace FirstAMLLibrary
{
    public class Step4
    {
        public string GetSendCostsWithSpeedyAndWeightLimitSurchargeTotal(ErrorReporter reporter, bool speedyOption, ParcelCostCalculator calculator, params Parcel[] parcels)
        {
            var classifier = new ParcelClassifier();
            var outputBuilder = new StringBuilder();
            var totalCost = 0.0;
            foreach (var parcel in parcels)
            {
                var classification = classifier.GetClassification(parcel, reporter);
                var cost = calculator.CalculateUnitCost(parcel, classifier, reporter);
                outputBuilder.AppendFormat("{0}\t{1}\t{2}\n", parcel.Id, classification, cost);
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
