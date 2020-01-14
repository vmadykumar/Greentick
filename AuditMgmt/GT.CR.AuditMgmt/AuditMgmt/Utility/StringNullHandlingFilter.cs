using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditMgmt.Utility
{
    public class StringNullHandlingFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult)
            {
                var viewResult = context.Result as ObjectResult;
                viewResult.Value = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(viewResult.Value).Replace("\":null", "\":\"\""));
            }
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // throw new NotImplementedException();
        }


    }
}

