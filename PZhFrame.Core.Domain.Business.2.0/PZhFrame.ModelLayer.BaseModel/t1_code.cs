using PZhFrame.Data.Repository.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace PZhFrame.ModelLayer.BaseModels
{
    public class t1_code:BaseModel
    {
        [ExplicitKey]
        public int id { get; set; }
        public string name { get; set; }
    }
}
