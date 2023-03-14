using System.Reflection;

namespace FirstAMLLibrary
{
    public class ParcelCostCalculator
    {
        private static readonly Dictionary<string, double> dimensionCost;
        private static readonly Dictionary<string, double> dimensionWeightLimit;
        private static readonly Dictionary<string, double> overWeightSurcharge;

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

            overWeightSurcharge = new Dictionary<string, double>();
            foreach (var prop in typeof(ParcelOverweightSurcharge).GetProperties(BindingFlags.Static | BindingFlags.NonPublic))
            {
                if (prop.PropertyType.FullName == "System.String")
                {
                    string? valueString = (string?)prop.GetValue(null);
                    if (valueString != null)
                    {
                        if (double.TryParse(valueString, out double value))
                        {
                            overWeightSurcharge.Add(prop.Name, value);
                        }
                    }
                }
            }
        }

        private bool useHeavyCategory;

        public ParcelCostCalculator(bool useHeavyCategory)
        {
            this.useHeavyCategory = useHeavyCategory;
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
                    // Calculate regular overweight surcharge
                    var regularCost = dimensionCost[classification];
                    if (parcel.Weight > weightLimit)
                    {
                        var surcharge = overWeightSurcharge[classification];
                        regularCost += surcharge * (parcel.Weight - weightLimit);
                    }
                    // Calculate based on heavy surcharge
                    if (useHeavyCategory &&
                        dimensionCost.TryGetValue("Heavy", out double heavyCost) &&
                        dimensionWeightLimit.TryGetValue("Heavy", out double heavyWeightLimit) &&
                        overWeightSurcharge.TryGetValue("Heavy", out double heavySurcharge))
                    {
                        if (parcel.Weight > heavyWeightLimit)
                        {
                            heavyCost += heavySurcharge * (parcel.Weight - heavyWeightLimit);
                        }
                        return Math.Min(regularCost, heavyCost);
                    }
                    return regularCost;
                }
            }
            error.Report(parcel, "failed to calculate unit cost of parcel");
            return double.NaN;
        }
    }
}
