using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xktec.hqfq.Entity
{
    public class UserInfo : BaseEntity
    {
     
        public virtual string LoginName { get; set; }
    
        public virtual string Password { get; set; }
   
        public virtual string Name { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }

}
