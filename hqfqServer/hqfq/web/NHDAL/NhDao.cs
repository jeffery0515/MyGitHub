using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xktec.hqfq.Entity;
using NHibernate;
using NHibernate.Linq;
using System.Web;

namespace Xktec.hqfq.DAL
{
   public class NhDao<T> where T:BaseEntity
    {
       private ISessionFactory sessionFactory = HqfqSessionFactory.GetCurrentFactory();
       private ISession session;
       public NhDao()
       {
           try
           {
               session =sessionFactory.GetCurrentSession();
           }
           catch(Exception e)
           {
               session = sessionFactory.OpenSession();  
           }
          
       }

       public T Get(Guid id)
       {
           return session.Query<T>().Where(c => c.Id == id).FirstOrDefault();
       }
       public void  Add(T  entity)
       {
           session.Save(entity);
           session.Flush();
       
       }
       public void Update(T entity)
       {
           session.Update(entity);
       }
       public void Delete(Guid id)
       {
           session.Delete(Get(id));
       }
       public IQueryable<T> GetAll()
       {
            return session.Query<T>();
       }
    }
}
