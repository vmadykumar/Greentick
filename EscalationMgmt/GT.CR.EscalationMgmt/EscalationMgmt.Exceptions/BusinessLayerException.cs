namespace EscalationMgmt.Exceptions
{
    public class BusinessLayerException : CustomException
    {


        public BusinessLayerException()
            : base()
        {

        }

        public BusinessLayerException(string message)
         : base(message)
        {

        }

        public BusinessLayerException(string message, System.Exception inner)
         : base(message, inner)
        {

        }
    }
}
