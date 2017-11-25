using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;



namespace GAM.Controllers
{
    public class BaseController : Controller
    {
        public override Task OnActionExecutionAsync(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context, Microsoft.AspNetCore.Mvc.Filters.ActionExecutionDelegate next)
        {
            var prevUrl = Request.Headers["Referer"].ToString();
            if (prevUrl == "")
                prevUrl = Url.Action("Index", "Home");
            ViewBag.PreviousUrl = prevUrl;
            
            return base.OnActionExecutionAsync(context, next);
        }
    }
}