using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xktec.hqfq.Common;
using web;
using  Xktec.hqfq.Biz;
using Xktec.hqfq.Entity;

namespace web.Controllers
{
    public class LineController : Controller
    {
     
       
        private LineBiz lineBiz = new LineBiz();
        private CategoryBiz categoryBiz = new CategoryBiz();
     
        public ActionResult Index(int id = 1)
        {
           
           
            return View("list2");
        }
        public ActionResult Demo(int id)
        {
            return View("list2");
        }
        public ActionResult List(LineState? state, Guid? category, int p = 1)
        {
            return View();

        }
        public JsonResult LineJson(Guid? lineId,LineState? state, Guid? category,int pageSize=10,int pageIndex=0)
        {
          if(lineId!=null)
          {
           
             var line=lineBiz.Get((Guid)lineId);
            return  Json(line,JsonRequestBehavior.AllowGet);
          }
         
          String name = Request["name"]??"";
         IQueryable<LineInfo> quest = lineBiz.Search(state, category,name);

          var lines = quest.OrderBy(c => c.CreateTime).Skip((pageIndex) * pageSize)
              .Take(pageSize).ToList()
              .Select(c => new web.Model.LineSimple4Op
              {
                  Id = c.Id,
                  Name = c.Name,
                  Category = c.Category.Name,
                  CreateTime = c.CreateTime.ToString(),
                  Day = c.Day,
                  Image = c.Image == null ? "" : "../../image/index/" + c.Image.Id,
                  State =(c.IsRecommended?"reco":(c.IsShow?"show":"close")),
                  Order=c.PostOrder,
                  Adwords=c.AdWords
 
              });
            
            return Json(new {total=quest.Count(),data=lines }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            List<SelectListItem> cate = new List<SelectListItem>();
            foreach (var item in categoryBiz.GetAll())
            {
                cate.Add(new SelectListItem { Selected = false, Text = item.Name, Value = item.Id.ToString() });
            }

            ViewBag.cate = cate;
            return View();
        }
        [HttpPost]
        public ActionResult Create(LineInfo lineInfo)
        {
            lineBiz.Add(lineInfo);
            return RedirectToAction("");
        }

        public ActionResult Edit(Guid id)
        {
            var q = lineBiz.Get(id);
            List<SelectListItem> cate = new List<SelectListItem>();
            foreach (var item in categoryBiz.GetAll())
            {
                if (q.Category != null && item.Id == q.Category.Id)
                    cate.Add(new SelectListItem { Selected = true, Text = item.Name, Value = item.Id.ToString() });
                else cate.Add(new SelectListItem { Selected = false, Text = item.Name, Value = item.Id.ToString() });

            }
            ViewBag.cate = cate;
            return View(q);
        }
        [HttpPost]
        public ActionResult Edit(LineInfo lineInfo)
        {
            lineBiz.Update(lineInfo);
            return View();
        }
        [HttpPost]
        public string Op(Guid id, string op)
        {
            if (id == null || String.IsNullOrWhiteSpace(op))
                return "操作失败，参数错误";
            var operate = op.Trim().ToLower();
            if (operate.Equals("close"))
            {
                lineBiz.Close(id);
                return "关闭成功";
            }
            if (operate.Equals("show"))
            {
                lineBiz.Show(id);
                return "显示成功";
            }
            if (operate.Equals("re"))
            {
                lineBiz.Recom(id);
                return "推荐成功";
            }
            if (operate.Equals("unre"))
            {
                lineBiz.UnRecom(id);
                return "取消推荐成功";
            }
            if(operate.Equals("post"))
            {
                lineBiz.Post(id);
                return "设为顶部项目成功";
            }
            if (operate.Equals("unpost"))
            {
                lineBiz.UnPost(id);
                return "取消成功";
            }
           

            return "操作失败，参数错误";


        }
        public ActionResult Post()
        {

            return View();
        }
        [HttpPost]
        public String Post(Guid lineId, string op)
        {
            if(lineId==null||String.IsNullOrWhiteSpace(op))
            {
                return  "参数错误！";
            }
            else if(op.Equals("add"))
            {
                lineBiz.Post(lineId);
               
                
                return "添加成功！";
            }
            else if(op.Equals("remove"))
            {
                lineBiz.UnPost(lineId);
                return "移除成功";
            } 
            return "";
        }
        public JsonResult PostList()
        {
            var line = lineBiz.Search(LineState.首部广告,null);
          return Json(line.Select(c => new { id = c.Id, title = c.PostTilte, image = c.PostImage.Id, order = c.PostOrder }).OrderBy(c => c.order).ToList());
        }
        

    }
}