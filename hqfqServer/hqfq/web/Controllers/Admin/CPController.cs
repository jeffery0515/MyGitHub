using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xktec.hqfq.Common;
using Xktec.hqfq.Entity;
namespace web.Controllers.Admin
{
    public class CPController : Controller
    {
        //
        // GET: /CP/
         [CustomerAuthorize]
        public ActionResult Index()
        {
            return View();
        }
         public ActionResult Desktop()
         {
             return View();
         }

    }
}
