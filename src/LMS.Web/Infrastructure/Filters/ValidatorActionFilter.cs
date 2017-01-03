using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LMS.Web.Infrastructure.Filters
{
    public class ValidatorActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ModelState.IsValid)
                return;

            if (filterContext.HttpContext.Request.Method == "GET")
            {
                var result = new BadRequestResult();
                filterContext.Result = result;
            }
            else
            {
                filterContext.HttpContext.Response.StatusCode = 400;
                filterContext.Result = CustomHttpResults.BadModelState(filterContext.ModelState);
            }
        }
    }
}
