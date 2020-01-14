using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditMgmt.Excepetions
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
