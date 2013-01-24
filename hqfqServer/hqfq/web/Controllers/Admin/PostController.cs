using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xktec.hqfq.Biz;
namespace web.Controllers
{
    public class PostController : Controller
    {

        private LineBiz lineBiz = new LineBiz();
     public ActionResult Index()
      {
           
            return View();
       }
     [HttpPost]
     public String Add(Guid[] id)
     {
       foreach (var item in id)
	        {
                lineBiz.Post(item);	 
	        }
       return "";
     }
        [HttpPost]
        public string Edit()
        {
            Guid id = Guid.Parse(Request["id"]);
            string title = Request["postTitle"];
            int order = Int32.Parse(Request["order"]);
            Guid  imageId=Guid.Parse(Request["imageId"]);
            lineBiz.SetPost(id, title, imageId,order);
            return "";
        }

    }
}
