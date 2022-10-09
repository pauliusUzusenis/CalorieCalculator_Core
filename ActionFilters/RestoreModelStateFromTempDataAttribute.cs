using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CalorieCalculator.ActionFilters
{
    public abstract class ModelStateTransfer : ActionFilterAttribute
    {
        protected const string Key = nameof(ModelStateTransfer);

    }
    public class RestoreModelStateFromTempDataAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (filterContext.Controller != null)
            {
                if (((Controller)filterContext.Controller).TempData.ContainsKey("ModelState"))
                {
                    ((Controller)filterContext.Controller).ViewData.ModelState.Merge((ModelStateDictionary)((Controller)filterContext.Controller).TempData["ModelState"]);
                }
            }
        }
    }
}
