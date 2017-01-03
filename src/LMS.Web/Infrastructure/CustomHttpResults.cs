using System.Linq;
using LMS.Web.Infrastructure.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace LMS.Web.Infrastructure
{
    public static class CustomHttpResults
    {
        public static ContentResult BadModelState(ModelStateDictionary modelState)
        {
            var result = new ContentResult();
            var errors = modelState.ToDictionary(
                pair => JsonNetStringUtils.ToSnakeCase(pair.Key),
                pair => pair.Value.Errors.Select(x => x.ErrorMessage).ToArray());

            result.Content = JsonConvert.SerializeObject(new { errors });
            result.ContentType = "application/json";
            result.StatusCode = 400;

            return result;
        }
    }
}
