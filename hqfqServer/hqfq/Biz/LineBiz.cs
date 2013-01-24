using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xktec.hqfq.Common;
using  Xktec.hqfq.Entity;
using Xktec.hqfq.DAL;

namespace Xktec.hqfq.Biz
{
 public    class LineBiz
    {
     private NhDao<LineInfo> lineDao;
     public LineBiz()
     {
         lineDao = new NhDao<LineInfo>();
     }
     public LineInfo Get(Guid id)
     {
       return   lineDao.Get(id);
     }


     public IQueryable<LineInfo> Search(LineState? state, Guid? category, int pageSize = 10, int pageIndex = 0)
     {
         var quest = lineDao.GetAll();
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


    }
}
