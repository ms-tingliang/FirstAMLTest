namespace FirstAMLLibrary
{
    public struct Parcel
    {
        public string Id;
        public double DimensionInCentimeter;

        public Parcel(string id, double DimensionInCentimeter)
        {
            this.Id = id;
            this.DimensionInCentimeter = DimensionInCentimeter;
        }

        public Parcel(double DimensionInCentimeter)
        {
            this.Id = Guid.NewGuid().ToString();
            this.DimensionInCentimeter = DimensionInCentimeter;
        }
    }
}
