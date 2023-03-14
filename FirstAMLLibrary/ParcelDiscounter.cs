namespace FirstAMLLibrary
{
    public interface IDiscountRule
    {
        void Accumulate(Parcel parcel, string classification, double cost);
        IEnumerable<IList<Tuple<string, double>>> Discounts { get; }
    }

    static class Helper
    {
        public static double Max(IEnumerable<double> values)
        {
            var maxValue = double.NaN;
            foreach (var value in values)
            {
                if (double.IsNaN(maxValue) && !double.IsNaN(value) || maxValue > value)
                    maxValue = value;
            }
            return maxValue;
        }
    }

    public class SmallParcelMania : IDiscountRule
    {
        private IList<Tuple<string, double>> parcelCosts = new List<Tuple<string, double>>();
        public void Accumulate(Parcel parcel, string classification, double cost)
        {
            if (classification == "Small")
            {
                parcelCosts.Add(Tuple.Create(parcel.Id, cost));
            }
        }
        public IEnumerable<IList<Tuple<string, double>>> Discounts
        {
            get
            {
                var count = 0;
                var group = new List<Tuple<string, double>>();
                foreach (var parcelCost in from p in parcelCosts orderby p.Item2 ascending select p)
                {
                    group.Add(parcelCost);
                    if (++count % 4 == 0)
                    {
                        yield return group; // group are sorted in ascending order
                        group.Clear();
                    }
                }
            }
        }
    }

    public class MediumParcelMania : IDiscountRule
    {
        private IList<Tuple<string, double>> parcelCosts = new List<Tuple<string, double>>();
        public void Accumulate(Parcel parcel, string classification, double cost)
        {
            if (classification == "Medium")
            {
                parcelCosts.Add(Tuple.Create(parcel.Id, cost));
            }
        }
        public IEnumerable<IList<Tuple<string, double>>> Discounts
        {
            get
            {
                var count = 0;
                var group = new List<Tuple<string, double>>();
                foreach (var parcelCost in from p in parcelCosts orderby p.Item2 ascending select p)
                {
                    group.Add(parcelCost);
                    if (++count % 3 == 0)
                    {
                        yield return group; // group are sorted in ascending order
                        group.Clear();
                    }
                }
            }
        }
    }

    public class MixedParcelMania : IDiscountRule
    {
        private IList<Tuple<string, double>> parcelCosts = new List<Tuple<string, double>>();
        public void Accumulate(Parcel parcel, string classification, double cost)
        {
            parcelCosts.Add(Tuple.Create(parcel.Id, cost));
        }
        public IEnumerable<IList<Tuple<string, double>>> Discounts
        {
            get
            {
                var count = 0;
                var group = new List<Tuple<string, double>>();
                foreach (var parcelCost in from p in parcelCosts orderby p.Item2 ascending select p)
                {
                    group.Add(parcelCost);
                    if (++count % 5 == 0)
                    {
                        yield return group; // group are sorted in ascending order
                        group.Clear();
                    }
                }
            }
        }
    }

    public class ParcelDiscounter
    {
        IDictionary<string, double> parcelDiscounts;
        
        public void CalculateDiscount(ParcelClassifier classifier, ParcelCostCalculator calculator, ErrorReporter reporter, params Parcel[] parcels)
        {
            var rules = new List<IDiscountRule>() { new SmallParcelMania(), new MediumParcelMania(), new MixedParcelMania() };
           
            foreach (var parcel in parcels)
            {
                var classification = classifier.GetClassification(parcel, reporter);
                var cost = calculator.CalculateUnitCost(parcel, classifier, reporter);
                foreach (var rule in rules)
                {
                    rule.Accumulate(parcel, classification, cost);
                }
            }

            // Go through each group returned from the rules
            var parcelPotentialDiscounts = new Dictionary<string, double[]>();
            for (int i = 0; i < rules.Count; ++i)
            {
                foreach (var group in rules[i].Discounts)
                {
                    bool applied = false;
                    foreach (var parcelCost in group)
                    {
                        if (parcelPotentialDiscounts.ContainsKey(parcelCost.Item1))
                        {
                            for (var j = 0; j < rules.Count; ++j)
                            {
                                if (j == i)
                                {
                                    parcelPotentialDiscounts[parcelCost.Item1][j] = parcelCost.Item2;
                                    applied = true;
                                }
                            }
                        }
                        else
                        {
                            parcelPotentialDiscounts[parcelCost.Item1] = new double[rules.Count];
                            for (var j = 0; j < rules.Count; ++j)
                            {
                                if (j == i)
                                {
                                    parcelPotentialDiscounts[parcelCost.Item1][j] = parcelCost.Item2;
                                    applied = true;
                                }
                                else
                                    parcelPotentialDiscounts[parcelCost.Item1][j] = double.NaN;
                            }
                        }
                        if (applied)
                            break;
                    }
                }
            }

            // Go through potential discounts and ensure we apply the best discount for each item
            parcelDiscounts = new Dictionary<string, double>();
            foreach (var pair in parcelPotentialDiscounts)
            {
                parcelDiscounts[pair.Key] = Helper.Max(pair.Value);
            }
        }

        public double GetDiscount(Parcel parcel)
        {
            if (parcelDiscounts != null)
            {
                if (parcelDiscounts.TryGetValue(parcel.Id, out double discount))
                {
                    return discount;
                }
            }
            return double.NaN;
        }
    }
}
