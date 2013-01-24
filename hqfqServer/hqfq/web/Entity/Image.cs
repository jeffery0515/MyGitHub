using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xktec.hqfq.Entity
{
    public class Image:BaseEntity
    {
        public virtual String OriginalName { get; set; }
        public virtual String Name { get; set; }
        public virtual String Description { get; set; }
        public virtual int Type { get; set; }
        public virtual String Ext { get; set; }
        public  virtual Category Category { get; set; }
    
    }
}