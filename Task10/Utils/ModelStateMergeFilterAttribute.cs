using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Task10.Utils;

public class ModelStateMergeFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        // write to the temp data if there is modelstate BUT there is no tempdata key
        // this will allow it to be merged later on redirect
        
        var controller = filterContext.Controller as Controller;
        
        if (controller?.TempData["ModelState"] == null && controller?.ViewData.ModelState != null)
        {
            controller.TempData["ModelState"] = controller.ViewData.ModelState;
        }
        // if there is tempdata (from the previous action) AND its not the same instance as the current model state THEN merge it with the current model
        else if (controller?.TempData["ModelState"] != null && !controller.ViewData.ModelState.Equals(controller.TempData["ModelState"]))
        {
            controller.ViewData.ModelState.Merge((ModelStateDictionary)controller.TempData["ModelState"]);
        }
        base.OnActionExecuted(filterContext);
    }
}
