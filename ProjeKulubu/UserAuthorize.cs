using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjeKulubu
{
    public class UserAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Request.Cookies["UserName"] != null && httpContext.Request.Cookies["PassWord"] != null)
            {
                return true;
            }
            else
            {
                httpContext.Response.Redirect("/Admin/Login");
                return false;
            }
        }
    }
}