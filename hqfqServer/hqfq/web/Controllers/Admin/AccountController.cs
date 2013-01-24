using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xktec.hqfq.Common;
using web;
using System.Web.Security;
using System.Text;
using Xktec.hqfq.Entity;
using web.Model;
namespace web.Controllers.Admin
{
    public class AccountController : Controller
    {
       // private EfDbContext db = new EfDbContext();

        //public ActionResult Login(LoginModel loginModel)
        //{
        //    return View();
        //}

      
        //public ActionResult Login(LoginModel loginModel)
        //{
        //    if(loginModel.Password==""||loginModel.LoginName=="")
        //    return View(loginModel);

        //    try
        //    {
        //        var user = db.Users.Where(u => u.LoginName == loginModel.LoginName && u.Password == loginModel.Password).First();
              
        //            user.Roles.Select(c => c.Name).ToArray();
        //            StringBuilder sb = new StringBuilder();
        //            foreach (var item in user.Roles)
        //            {
        //                sb.Append(item.Name);
        //                sb.Append(",");

        //            }
        //            if(sb.Length>0)
        //            sb.Remove(sb.Length - 1, 1);

                
        //            FormsAuthentication.SetAuthCookie(user.Name, loginModel.RememberMe, FormsAuthentication.FormsCookiePath);
        //            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.Name, DateTime.Now, DateTime.Now.AddDays(1), true, sb.ToString());
        //            FormsIdentity identity = new FormsIdentity(ticket);
        //            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
        //            Response.Cookies.Add(cookie);
               
        //            return RedirectToAction("index", "cp", null);                
        //    }
        //    catch (Exception)
        //    {
        //        Response.Write("<script language='javascript'> alert('用户名或密码出错！')</script>");
        //        return View(loginModel);
        //    }
        //}
    }
}
