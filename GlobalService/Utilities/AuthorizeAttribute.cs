using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using GlobalService.DTO;
using GlobalService.Utilities;

namespace GlobalService.Utilities
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private Roles role;

        public AuthorizeAttribute(Roles role = Roles.User)
        {
            this.role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (UserDTO)context.HttpContext.Items["user"];
            if (user == null)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }

            Roles userRole = (Roles)Enum.Parse(typeof(Roles), user.Role);
            if(userRole < role)
            {
                context.Result = new JsonResult(new { message = "Unsufficient permission" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}