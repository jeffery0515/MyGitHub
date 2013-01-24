using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Xktec.hqfq.Common;
using Xktec.hqfq.Entity;
using Xktec.hqfq.Biz;

namespace web.Controllers.Admin
{
    public class ImageController : Controller
    {
        public static string uploadPath = ConfigurationManager.AppSettings["uploadFloder"];

        private ImageBiz imageBiz = new ImageBiz();


        [HttpPost]
        public JsonResult Upload()
        {
            HttpPostedFileBase file = Request.Files[0];

            try
            {
               
                
                int type = 0;
                System.Drawing.Image bmp = System.Drawing.Bitmap.FromStream(file.InputStream);
                string name = file.FileName.Remove(file.FileName.LastIndexOf('.'));
                Image image = new Image();
                Int32.TryParse(Request["type"], out type);
                image.Type = type;
                image.OriginalName = name;
                image.Id = Guid.NewGuid();
                image.CreateTime = DateTime.Now;
                if (String.IsNullOrWhiteSpace(Request["category"])||Request["category"]=="undefined")
                {
                    image.Category = null; 
                }
                else
                {
                    image.Category = new Category { Id = Guid.Parse(Request["categoryId"]) };
                }
                bmp.Save(Server.MapPath(uploadPath) + image.Id);

               

               
                imageBiz.Add(image);

                return Json(new { success = true, name = name, id = image.Id, height = bmp.Height, width = bmp.Width, type = type });

            }
            catch (Exception e)
            {
            }
            return Json(new { success = false });
        }

        public JsonResult list()
        {   Guid? id=null;
            if(!String.IsNullOrWhiteSpace(Request["id"]))
                   id = Guid.Parse(Request["id"]);
            int type = 0;
            Int32.TryParse(Request["type"], out type);
            var images= imageBiz.Search(id, type).Select(c=>c.Id);
            return Json(images, JsonRequestBehavior.AllowGet);


        }

        public ActionResult Thumbnail(Guid id)
        {
            //  var s=  db.Images.Where(c => c.Id == id).FirstOrDefault().Ext;
            //    byte[] img = ImageHandler.GenerateThumbnail(Server.MapPath(uploadPath + id+"."+s),120, 100);
            //    return File(img, @"image/jpeg");
            //}
            //public System.Web.Mvc.JsonResult GetImagesByCategory(Guid id)
            //{
            //    var s = db.Images.Where(c => c.Category.Id == id).OrderBy(c=>c.CreateTime).Select(c=>c.Id).ToList();
            return Json("", JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult Index(Guid id)
        {

            if (System.IO.File.Exists(Server.MapPath(uploadPath) + id))

                return File(Server.MapPath(uploadPath) + id, @"image/png");
            else
                return null;
        }


        [HttpPost]
        public string Crop(Guid id)
        {
            int viewPortW = 0;
            int viewPortH = 0;
            int imageX = 0;
            int imageY = 0;
            int imageRotate = 0;
            int imageW = 0;
            int imageH = 0;

            int selectorX = 0;
            int selectorY = 0;
            int selectorW = 0;
            int selectorH = 0;
            try
            {
                viewPortW = (int)float.Parse(Request["viewPortW"]);
                viewPortH = (int)float.Parse(Request["viewPortH"]);
                imageX = (int)float.Parse(Request["imageX"]);
                imageY = (int)float.Parse(Request["imageY"]);
                imageRotate = (int)float.Parse(Request["imageRotate"]);
                imageW = (int)float.Parse(Request["imageW"]);
                imageH = (int)float.Parse(Request["imageH"]);

                selectorX = (int)float.Parse(Request["selectorX"]);
                selectorY = (int)float.Parse(Request["selectorY"]);
                selectorW = (int)float.Parse(Request["selectorW"]);
                selectorH = (int)float.Parse(Request["selectorH"]);
            }
            catch (Exception e)
            {
            }
            var ms = ImageHandler.CropResizeRotate(Server.MapPath(uploadPath) + id.ToString(), viewPortW, viewPortH, imageX, imageY, imageW, imageH, imageRotate, selectorX, selectorY, selectorW, selectorH);
            System.IO.File.WriteAllBytes(Server.MapPath(uploadPath) + id.ToString(), ms);
            return id.ToString();

        }


    }



}
