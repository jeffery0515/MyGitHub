using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xktec.hqfq.EfDal;
using Xktec.hqfq.Entity;
namespace Xktec.hqfq.Biz
{
    public class ImageBiz
    {
        private EfDbContext db;
           CategoryBiz categoryBiz;
        public ImageBiz()
        {
           db = new EfDbContext();
           categoryBiz = new CategoryBiz(db);
        }
        public ImageBiz(EfDbContext dbcontext)
        {
            db = dbcontext;
            categoryBiz = new CategoryBiz(db);
        }
       
     
        public Image Get(Guid id)
        {
            return db.Images.Where(c => c.Id == id).FirstOrDefault();
        }
        public IQueryable<Image> GetAll()
        {
            return db.Images;
        }
        public void Add(Image image)
        {
            if (image.Category != null)
            {
                image.Category = categoryBiz.Get(image.Category.Id);
            }
            db.Images.Add(image);
            db.SaveChanges();
        }
        public IQueryable<Image> Search(Guid? categoryId, int type = 0)
        {
            var images = GetAll().Where(c => c.Type == type);
            if (categoryId != null)
            {
                images = images.Where(c => c.Category.Id == categoryId);
            }
            return images.OrderByDescending(c => c.CreateTime);
        }
    }
}