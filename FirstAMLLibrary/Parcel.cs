namespace FirstAMLLibrary
{
    public struct Parcel
    {
        public string Id;
        public double DimensionInCentimeter;
        public double Weight;

        public Parcel(string id, double DimensionInCentimeter, double Weight)
        {
            this.Id = id;
            this.DimensionInCentimeter = DimensionInCentimeter;
            this.Weight = Weight;
        }

        public Parcel(string id, double DimensionInCentimeter)
        {
            this.Id = id;
            this.DimensionInCentimeter = DimensionInCentimeter;
            this.Weight = double.NaN;
        }

        public Parcel(double DimensionInCentimeter)
        {
            this.Id = Guid.NewGuid().ToString();
            this.DimensionInCentimeter = DimensionInCentimeter;
            this.Weight = double.NaN;
        }
    }
}
