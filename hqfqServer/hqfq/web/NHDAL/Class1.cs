using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Cfg;
using System.Web;

namespace Xktec.hqfq.DAL
{
    public static class HqfqSessionFactory
    {


       public static ISessionFactory GetCurrentFactory()
        {
            if (sessionFactory == null)
            {
                sessionFactory = CreateSessionFactory();
            }

            return sessionFactory;
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Mappings(c => c.FluentMappings.AddFromAssembly(typeof(HqfqSessionFactory).Assembly))
                .CurrentSessionContext<NHibernate.Context.WebSessionContext>()
                .Database(
                    FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2008
                        .ConnectionString(c=>c.Server(".").Database("sss").TrustedConnection()
                        )
                ).BuildSessionFactory();
        }
        private static ISessionFactory sessionFactory
        {
            get;
            set;
        }        


    }
}
