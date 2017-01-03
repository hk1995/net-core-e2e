using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LMS.Web.Infrastructure.Filters
{
    public class MultipartContentValidatorActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!IsMultipartContentType(context.HttpContext.Request.ContentType))
            {
                context.Result = new StatusCodeResult(415);
                return;
            }

            // the model only binds to IFormFile so we have to manually set other properties
            if (context.ActionArguments.ContainsKey("model"))
            {
                var model = context.ActionArguments["model"] as IImageType;
                if (model != null)
                {
                    if (context.HttpContext.Request.Form.ContainsKey("image_type"))
                        model.ImageType = context.HttpContext.Request.Form["image_type"].ToString();
                }
            }

            base.OnActionExecuting(context);
        }

        private static bool IsMultipartContentType(string contentType)
        {
            return !string.IsNullOrEmpty(contentType) && 
                contentType.IndexOf("multipart/", StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
