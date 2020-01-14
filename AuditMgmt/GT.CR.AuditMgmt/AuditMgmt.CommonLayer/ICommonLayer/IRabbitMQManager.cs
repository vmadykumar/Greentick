using System;
using System.Collections.Generic;
using System.Text;

namespace AuditMgmt.CommonLayer.CommonImplementationLayer
{
    public interface IRabbitMQManager
    {
        void Publish(string message, IDictionary<string, object> arguments = null);
        void Receive(Func<string, IServiceProvider, object> process, IDictionary<string, object> arguments = null, IServiceProvider serviceProvider = null);
    }
}
