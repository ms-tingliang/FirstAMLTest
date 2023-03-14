namespace FirstAMLLibrary
{
    public class ErrorReporter
    {
        public IList<string> Errors;

        public ErrorReporter()
        {
            Errors = new List<string>();
        }

        public void Clear()
        {
            Errors.Clear();
        }

        public void Report(Parcel parcel, string message)
        {
            Errors.Add(string.Format("Error processing Parcel {0}, {1}",parcel.Id, message));
        }
    }
}
