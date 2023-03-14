namespace FirstAMLLibrary
{
    public struct Parcel
    {
        public Guid Id;
        public double DimensionInCentimeter;
        public Parcel(double DimensionInCentimeter)
        {
            this.Id = Guid.NewGuid();
            this.DimensionInCentimeter = DimensionInCentimeter;
        }
    }
}
