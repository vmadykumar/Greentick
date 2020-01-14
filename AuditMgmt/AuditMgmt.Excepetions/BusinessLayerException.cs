using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditMgmt.Excepetions
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
