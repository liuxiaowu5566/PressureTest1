using PZhFrame.Data.Repository.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace PZhFrame.ModelLayer.BaseModels
{
    public class t1_resource:BaseModel
    {
        [ExplicitKey]
        public Int64 id { get; set; }
        public DateTime createtime { get; set; }
    }
}
