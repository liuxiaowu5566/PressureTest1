using PZhFrame.Data.Repository.Extension;
using PZhFrame.ModelLayer.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Model.t1
{
    public class t1_resource:BaseModel
    {
        [ExplicitKey]
        public Int64 id { get; set; }
        public DateTime createtime { get; set; }
    }
}
