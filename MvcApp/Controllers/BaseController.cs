using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApp.Controllers
{
    public class BaseController:Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers.ContainsKey("User"))
            {
                var useragent = context.HttpContext.Request.Headers["User"].FirstOrDefault();
                // сравниваем его значение
                if (useragent.Contains("MSIE") || useragent.Contains("Trident"))
                {
                    context.Result = Content("Internet Explorer не поддерживается");
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
