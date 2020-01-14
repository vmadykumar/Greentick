using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AuditMgmt.Excepetions
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
    public class UserNotLoggedIn : Exception
    {
        public UserNotLoggedIn() : base()
        {
            
        }
        public UserNotLoggedIn(string message) : base(message)
        {

        }
        public UserNotLoggedIn(string message, Exception innerException) : base(message, innerException)
        {

        }
        public UserNotLoggedIn(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
