namespace EscalationMgmt.Exceptions
{
    public class DataLayerException : CustomException
    {
        public DataLayerException()
            : base()
        {
            // Add implemenation (if required)
        }

        public DataLayerException(string message)
         : base(message)
        {
            // Add implemenation (if required)
        }

        public DataLayerException(string message, System.Exception inner)
         : base(message, inner)
        {
            // Add implementation
        }

    }
}
