using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xktec.hqfq.Common;
using Xktec.hqfq.EfDal;
using Xktec.hqfq.Entity;


namespace Xktec.hqfq.Biz
{
    public class CategoryBiz
    {
        private EfDbContext db;

        public CategoryBiz()
        {
            db = new EfDbContext();
        }
        public CategoryBiz(EfDbContext dbcontext)
        {
            db = dbcontext;
        }

        public Category Get(Guid id)
        {
            return db.Categories.Where(c => c.Id == id).FirstOrDefault();
        }
        public void Add(Category category)
        {
            category.Id = Guid.NewGuid();
            db.Categories.Add(category);
            db.SaveChanges();
        }
        public IQueryable<Category> GetAll()
        {
            return db.Categories;
        }

        public void Update(Category category)
        {
            var  oCategory=Get(category.Id);
            if (oCategory != null)
            {
                oCategory.Name = category.Name;
                oCategory.Description = category.Description;
                db.SaveChanges();
            }
        }
        public void Delete(Guid id)
        {
            var oCategory = Get(id);
            db.Categories.Remove(oCategory);
            db.SaveChanges();
        }

      

    }
}