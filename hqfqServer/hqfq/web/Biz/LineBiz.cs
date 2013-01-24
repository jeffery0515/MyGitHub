using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xktec.hqfq.Common;
using Xktec.hqfq.DAL;
using Xktec.hqfq.Entity;
using Xktec.hqfq.EfDal;
namespace Xktec.hqfq.Biz
{
    public class LineBiz
    {
        private EfDbContext db = new EfDbContext();
         private CategoryBiz categoryBiz;
         private ImageBiz imageBiz;
        public LineBiz(EfDbContext db)
        {
            this.db = db;
            categoryBiz = new CategoryBiz(db);
            imageBiz = new ImageBiz(db);
        }
        public LineBiz()
        {
            categoryBiz = new CategoryBiz(db);
            imageBiz = new ImageBiz(db);
        }
       

        public LineInfo Get(Guid id)
        {
            var line= db.Lines.Where(q => q.Id == id).FirstOrDefault();
       
          return line;
        }
        public IQueryable<LineInfo> GetAll()
        {
            return db.Lines;
        }
        public IQueryable<LineInfo> Search(LineState? state, Guid? category,String name="",int Day=-1)
        {
            IQueryable<LineInfo> quest = GetAll();
            if (!String.IsNullOrWhiteSpace(name))
            {
               quest= quest.Where(c=>c.Name.Contains(name));
            }
            if (Day != -1)
            {
                quest = quest.Where(c => c.Day == Day);
            }
            if (state == null || state == LineState.所有)
            {
            }
            else if (state == LineState.首部广告)
            {
                quest = quest.Where(c => c.IsPost == true);
            }
            else if (state == LineState.首页列表)
            {
                quest = quest.Where(c => c.IsRecommended == true);
            }
            else if (state == LineState.正常显示)
            {
                quest = quest.Where(c => c.IsShow == true);
            }
            else if (state == LineState.关闭)
            {
                quest = quest.Where(c => c.IsShow == false);
            }
            else if (state == LineState.正常且不在首页)
            {
                quest = quest.Where(c => c.IsShow == true && c.IsPost == false);
            }

            if (category != null)
            {
                quest = quest.Where(c => c.Category.Id == category);
            }
            return quest;
        }

        public Guid Add(LineInfo lineInfo)
        {
            lineInfo.Id = Guid.NewGuid();
            Guid imageId = lineInfo.Image.Id;
            lineInfo.Image = imageBiz.Get(lineInfo.Image.Id);
            lineInfo.CreateTime = DateTime.Now;
            lineInfo.Category = categoryBiz.Get(lineInfo.Category.Id);
            foreach (var item in lineInfo.Itineraries)
            {
                item.Id = Guid.NewGuid();
            }

            db.Lines.Add(lineInfo);
            db.SaveChanges();
            return lineInfo.Id;

        }

        public void Update(LineInfo lineInfo)
        {
            var oLine = Get(lineInfo.Id);
            var i = db.Itinerary.Where(c => c.Line.Id == lineInfo.Id);

            foreach (var item in i)
            {
                db.Itinerary.Remove(item);
            }
            oLine.Itineraries.Clear();
            foreach (var item in lineInfo.Itineraries)
            {
                item.Id = Guid.NewGuid();
                oLine.Itineraries.Add(item);
            }
            oLine.Category = categoryBiz.Get(lineInfo.Category.Id);
          
            
            oLine.AdWords = lineInfo.AdWords;
            oLine.Image = imageBiz.Get(lineInfo.Image.Id);
            oLine.Name = lineInfo.Name;
            oLine.OutCity = lineInfo.OutCity;
            oLine.SelfFincItems = lineInfo.SelfFincItems;
            oLine.Cautions = lineInfo.Cautions;
            oLine.ChargeExs = lineInfo.ChargeExs;
            oLine.ChargeIns = lineInfo.ChargeIns;
            oLine.Features = lineInfo.Features;
            oLine.Tips = lineInfo.Tips;
            db.SaveChanges();
        }
        public void Delete(Guid id)
        {
            db.Lines.Remove(Get(id));
            db.SaveChanges();
        }
        public void Show(Guid id)
        {
            var line = Get(id);
            if (line != null)
            {
                line.IsShow = true;
                db.SaveChanges();
            }
        }
        public void Close(Guid id)
        {
            var line = Get(id);
            if (line != null)
            {
                line.IsShow = false;
                line.IsRecommended = false;
                line.IsPost = false;
                db.SaveChanges();
            }
        }
        public void Recom(Guid id)
        {
            var line = Get(id);
            if (line != null)
            {
                line.IsShow = true;
                line.IsRecommended = true;
                db.SaveChanges();
            }

        }
        public void UnRecom(Guid id)
        {
            var line = Get(id);
            if (line != null)
            {
                line.IsRecommended = false;
                db.SaveChanges();
            }
        }
        public void Post(Guid id)
        {
            var line = Get(id);
            if (line != null)
            {
                line.IsShow = true;
                line.IsPost = true;
                db.SaveChanges();
            }
        }
        public void UnPost(Guid id)
        {
            var line = Get(id);
            if (line != null)
            {
                line.IsPost = false;
                db.SaveChanges();
            }
        }

        public void SetPost(Guid lineId, string title, Guid imageId, int Order = 0)
        {
            var line = Get(lineId);
            line.PostImage = imageBiz.Get(imageId);
            line.PostTilte = title;
            line.PostOrder = Order;
            db.SaveChanges();
        }


    }

}