using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.Core.Exception
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var responseObj = new ExceptionResultModel
                {
                    Message = new string[] {
                        Newtonsoft.Json.JsonConvert.SerializeObject(context.ModelState)
                        /*: "Dữ liệu không hợp lệ"*/ },
                    Data = null
                };

                context.Result = new JsonResult(responseObj)
                {
                    ContentType = "application/json",
                    StatusCode = 400
                };
            }
        }
    }
}
