using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using Spring.Data.NHibernate;
using NHibernate.Cfg;
using FluentNHibernate.Mapping.Providers;

namespace Xktec.hqfq.DAL
{
    public static class HqfqSessionFactory : Spring.Data.NHibernate.LocalSessionFactoryObject
    {
        protected override void PostProcessConfiguration(Configuration config)
        {
            base.PostProcessConfiguration(config);
            config.AddMappingsFromAssembly(typeof(HqfqSessionFactory).Assembly);
        }

       //public static ISessionFactory GetCurrentFactory()
       // {
       //     if (sessionFactory == null)
       //     {
       //         sessionFactory = CreateSessionFactory();
       //     }

       //     return sessionFactory;
       // }

       // private static ISessionFactory CreateSessionFactory()
       // {
       //     return Fluently.Configure()
       //         .Mappings(c => c.FluentMappings.AddFromAssembly(typeof(HqfqSessionFactory).Assembly))
       //         .CurrentSessionContext<NHibernate.Context.WebSessionContext>()
       //         .Database(
       //             FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2008
       //                 .ConnectionString(c=>c.FromConnectionStringWithKey
       //                 ("working")).DefaultSchema("hqfq")
       //         ).BuildSessionFactory();
       // }
       // private static ISessionFactory sessionFactory
       // {
       //     get;
       //     set;
       // }        


    }
}
