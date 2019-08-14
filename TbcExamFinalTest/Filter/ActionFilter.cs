using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace TbcExamFinalTest.Filter
{
    public class ActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                throw new Exception($"Model Is Not Valid {JsonConvert.SerializeObject(context.ModelState.Values.SelectMany(state => state.Errors).Select(error => error.ErrorMessage))}");
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
