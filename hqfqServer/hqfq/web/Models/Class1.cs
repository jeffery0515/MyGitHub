using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Model
{
    public class Class1
    {
    }
    public class LineSimlpeBase
    {
        private Guid id;
        private string name = "";
        private int day = 0;
        private string image = "";
        private string adwords = "";

        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Day
        {
            get { return day; }
            set { day = value; }
        }
        public string Image
        {
            get { return image; }
            set { image = value; }
        }
        public string Adwords
        {
            get { return adwords; }
            set { adwords = value; }
        }
    }
   
    public class LineSimple4Op:LineSimlpeBase
    {

     
        private string category = "";    
        private int order = 0;
        private string state = "";
        private string createTime = "";  
        private string adwords = "";

      
        public string Category
        {
            get { return category; }
            set { category = value; }
        }
     
        public int Order
        {
            get { return order; }
            set { order = value; }
        }
        public string State
        {
            get { return state; }
            set { state = value; }
        }
        public string CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        } 
    }
    public class LineSimple4Api : LineSimlpeBase
    {
        private string adwords = "";

        public string Adwords1
        {
            get { return adwords; }
            set { adwords = value; }
        }
        private int bizPrice = 0;

        public int BizPrice
        {
            get { return bizPrice; }
            set { bizPrice = value; }
        }
        private int customerPrice = 0;

        public int CustomerPrice
        {
            get { return customerPrice; }
            set { customerPrice = value; }
        }
    }
   
}