
using Microsoft.AspNetCore.Mvc.Filters;

namespace Project_To_Do_List.Areas.Admin.Attributes

{
    public class CheckLogin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string _email = context.HttpContext.Session.GetString("Email_login"); 
            if (string.IsNullOrEmpty(_email))
            {
                context.HttpContext.Response.Redirect("/Admin/Account/Login");
            }
            base.OnActionExecuting(context);
        }
    }
}
