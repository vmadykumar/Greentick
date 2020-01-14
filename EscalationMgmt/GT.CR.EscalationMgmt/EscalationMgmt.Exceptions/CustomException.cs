using System;
using System.Runtime.Serialization;

namespace EscalationMgmt.Exceptions
{
    public class CustomException : Exception
    {
      
        public CustomException()
            : base()
        {

        }

        public CustomException(string message)
         : base(message)
        {

        }

        public CustomException(string message, System.Exception inner)
         : base(message, inner)
        {

        }
    }

    [Serializable]
    public class UserNotLoggedInException : Exception
    {
        public UserNotLoggedInException() : base()
        {
            
        }
        public UserNotLoggedInException(string message) : base(message)
        {

        }
        public UserNotLoggedInException(string message, Exception innerException) : base(message, innerException)
        {

        }
        public UserNotLoggedInException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
