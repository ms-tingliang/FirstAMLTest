using System.Reflection;

namespace FirstAMLLibrary
{
    public class ParcelCostCalculator
    {
        private static readonly Dictionary<string, double> dimensionCost;
        private static readonly Dictionary<string, double> dimensionWeightLimit;
        private const double overweightSurcharge = 2.0;
        
        static ParcelCostCalculator()
        {
            dimensionCost = new Dictionary<string, double>();
            foreach (var prop in typeof(ParcelSendCost).GetProperties(BindingFlags.Static | BindingFlags.NonPublic))
            {
                if (prop.PropertyType.FullName == "System.String")
                {
                    string? valueString = (string?)prop.GetValue(null);
                    if (valueString != null)
                    {
                        if (double.TryParse(valueString, out double value))
                        {
                            dimensionCost.Add(prop.Name, value);
                        }
                    }
                }
            }

            dimensionWeightLimit = new Dictionary<string, double>();
            foreach (var prop in typeof(ParcelWeightLimit).GetProperties(BindingFlags.Static | BindingFlags.NonPublic))
            {
                if (prop.PropertyType.FullName == "System.String")
                {
                    string? valueString = (string?)prop.GetValue(null);
                    if (valueString != null)
                    {
                        if (double.TryParse(valueString, out double value))
                        {
                            dimensionWeightLimit.Add(prop.Name, value);
                        }
                    }
                }
            }
        }

        public double CalculateUnitCost(Parcel parcel, ParcelClassifier classifier, ErrorReporter error)
        {
            var classification = classifier.GetClassification(parcel, error);
            if (!string.IsNullOrEmpty(classification))
            {
                if (double.IsNaN(parcel.Weight) || !dimensionWeightLimit.TryGetValue(classification, out double weightLimit))
                {
                    return dimensionCost[classification];
                }
                else
                {
                    var cost = dimensionCost[classification];
                    if (parcel.Weight > weightLimit)
                    {
                        cost += overweightSurcharge * (parcel.Weight - weightLimit);
                    }
                    return cost;
                }
            }
            error.Report(parcel, "failed to calculate unit cost of parcel");
            return double.NaN;
        }
    }
}
