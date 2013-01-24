using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xktec.hqfq.Entity
{
  public  class Itinerary:BaseEntity
    {
        public virtual int OrderNum { get; set; }
        public virtual string Title { get; set; }
        public virtual string MainContent { get; set; }
        public virtual string Hotel { get; set; }
        public virtual bool HasBreakfast { get; set; }
        public virtual bool HasLunch { get; set; }
        public virtual bool HasDinner { get; set; }
        public virtual LineInfo Line { get; set; }
    }
}
