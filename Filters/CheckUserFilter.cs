using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HipercorWeb.Filters
{
    public class CheckUserFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string sUser = context.HttpContext.Session.GetString("User");
            if (sUser == null)
            {
                context.Result = new RedirectResult("/Tienda/Login");
            }
        }
    }
}
