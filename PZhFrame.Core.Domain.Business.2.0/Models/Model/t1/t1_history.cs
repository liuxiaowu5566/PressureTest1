using PZhFrame.ModelLayer.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Model.t1
{
    public class t1_history:BaseModel
    {
        public t1_history(bool newInstance) : base(newInstance)
        {


        }

        public t1_history()
        {


        }
        public int id { get; set; }
        public int codeid { get; set; }
        public int houseid { get; set; }
        public string value { get; set; }
        public DateTime createtime { get; set; }
        public Guid opreatorid { get; set; }
    }
}
