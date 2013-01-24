using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xktec.hqfq.Entity
{
  public abstract  class BaseEntity
    {
        public virtual Guid Id { get; set; }
        public virtual  DateTime? CreateTime { get; set; }
    }
}
