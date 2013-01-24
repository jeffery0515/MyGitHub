using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Xktec.hqfq.Entity;
using Xktec.hqfq.Biz;

namespace web.Controllers.Admin
{
    public class CategoryController : Controller
    {
        private CategoryBiz categoryBiz = new CategoryBiz();

        public ActionResult Index()
        {
          var d=  categoryBiz.GetAll();
            return View(d);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            categoryBiz.Add(category);
            return RedirectToAction("Index");
        }

        public JsonResult List()
        {
           var list = categoryBiz.GetAll().OrderByDescending(c=>c.CreateTime).Select(c => new { Id = c.Id, Name = c.Name }).ToList();
           return Json(list,JsonRequestBehavior.AllowGet);
        }

    }
}
