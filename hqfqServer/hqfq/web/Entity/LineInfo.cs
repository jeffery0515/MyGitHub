using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xktec.hqfq.Entity
{
    public class LineInfo : BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual string AdWords { get; set; }
        public virtual int Day { get; set; }
        public virtual string OutCity { get; set; }
        public virtual int ClickCount { get; set; }
        public virtual Category Category { get; set; }
        public virtual Image Image { get; set; }
        /// <summary>
        /// 是否已被删除
        /// </summary>
        public virtual Boolean IsShow { get; set; }
        /// <summary>
        /// 是否显示在客户端
        /// </summary>
        public virtual Boolean IsRecommended { get; set; }
        /// <summary>
        /// 行程信息 
        /// </summary>
        public virtual ICollection<Itinerary> Itineraries { get; set; }
        /// <summary>
        /// 注意事项，备注等 
        /// </summary>
        public virtual string AddInfo { get; set; }
        public virtual string Cautions
        {
            get;
            set;
        }
        public virtual string Features { get; set; }
        public virtual string ChargeExs { get; set; }
        public virtual string ChargeIns { get; set; }
        public virtual string Tips { get; set; }
        public virtual string SelfFincItems { get; set; }
        public virtual Boolean IsPost { get; set; }
        public virtual Image PostImage { get; set; }
        public virtual String PostTilte { get; set; }
        public virtual int PostOrder { get; set; }
     

    }

   
}
