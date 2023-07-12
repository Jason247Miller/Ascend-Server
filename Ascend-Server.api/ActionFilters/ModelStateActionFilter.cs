using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

//Action Filter to aggregate all model state validation errors into http response
namespace Ascend_Server.api.ActionFilters
{
    public class ModelStateActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {

            if (!context.ModelState.IsValid)
            {
                var errorList = new List<string>();

                foreach (var error in context.ModelState.Values)
                {
                    foreach (var subError in error.Errors)
                    {
                        errorList.Add(subError.ErrorMessage);
                    }
                }

                var response = new
                {
                    Message = "One or more validation errors occurred.",
                    Errors = errorList
                };

                context.Result = new BadRequestObjectResult(response);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do nothing
        }

    }

}


