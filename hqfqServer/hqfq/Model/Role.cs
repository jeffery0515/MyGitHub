using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xktec.hqfq.Entity
{
    public class Role:BaseEntity
    {
        public string Name { get; set; }
        public String  Description { get; set; }
        public ICollection<UserInfo> Users { get; set; }
    }
}