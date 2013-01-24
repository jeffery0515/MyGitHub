using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xktec.hqfq.Entity;
namespace web.Controllers.Admin
{
    public class UserController : Controller
    {
        //private EfDbContext db = new EfDbContext();

        public ActionResult Index()
        {
            //var u = db.Users.ToList();
            return View();
        }
        public ActionResult Create(UserInfo user)
        {
            if (user.LoginName == null || user.LoginName == "")
                return View(user);
            else
            {
                //user.CreateTime = DateTime.Now;
                
                //user.Id = Guid.NewGuid();
                //db.Users.Add(user);
                //db.SaveChanges();
                return RedirectToAction("login", "account", null);

            }
        }
       

    }
}
