using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CalorieCalculator.ActionFilters
{
    public class SetInvalidTempDataModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if (filterContext.Controller != null)
            {
                if (!((Controller)filterContext.Controller).ViewData.ModelState.IsValid)
                {
                    ((Controller)filterContext.Controller).TempData["ModelState"] = ((Controller)filterContext.Controller).ViewData.ModelState;
                }

                if (((Controller)filterContext.Controller).TempData.ContainsKey("ModelState"))
                {
                    ((Controller)filterContext.Controller).ViewData.ModelState.Merge((ModelStateDictionary)((Controller)filterContext.Controller).TempData["ModelState"]);
                }
            }

            
        }
    }
}
