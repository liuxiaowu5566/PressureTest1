using PZhFrame.Data.Repository.Extension;
using PZhFrame.ModelLayer.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Model.t1
{
    public class t1_code:BaseModel
    {
        [ExplicitKey]
        public int id { get; set; }
        public string name { get; set; }
    }
}
