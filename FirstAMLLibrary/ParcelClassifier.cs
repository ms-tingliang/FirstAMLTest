using System.Reflection;

namespace FirstAMLLibrary
{
    public class ParcelClassifier
    {
        private static readonly SortedList<double, string> sortedDimensions;
        static ParcelClassifier()
        {
            sortedDimensions = new SortedList<double, string>();
            foreach (var prop in typeof(ParcelDimention).GetProperties(BindingFlags.Static | BindingFlags.NonPublic))
            {
                if (prop.PropertyType.FullName == "System.String")
                {
                    string? valueString = (string?)prop.GetValue(null);
                    if (valueString != null)
                    {
                        if (double.TryParse(valueString, out double value))
                        {
                            sortedDimensions.Add(value, prop.Name);
                        }
                        else
                        {
                            sortedDimensions.Add(double.MaxValue, prop.Name);
                            break;
                        }
                    }
                }
            }
        }

        public string GetClassification(Parcel parcel, ErrorReporter error)
        {
            foreach (var dimension in sortedDimensions)
            {
                if (parcel.DimensionInCentimeter < dimension.Key)
                {
                    return dimension.Value;
                }
            }
            error.Report(parcel, "failed to get classification of parcel");
            return string.Empty;
        }
    }
}
