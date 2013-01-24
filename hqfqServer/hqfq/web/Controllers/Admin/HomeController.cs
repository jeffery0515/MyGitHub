using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xktec.hqfq.Common;
using System.Web.Security;
namespace web.Controllers
{
    public class HomeController : Controller
    {
   //     private EfDbContext context = new EfDbContext();

      
        public ActionResult Index()
        {
            return RedirectToAction("index", "cp", null);
        }
      
    }
}
