using PZhFrame.ModelLayer.BaseModels;
using System;

namespace Models.Model.t1
{
    public class t1_house : BaseModel
    {
        public int id { get; set; }

        public DateTime createtime { get; set; }

        public Guid opreatorid { get; set; }
    }
}
