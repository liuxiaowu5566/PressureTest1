using PZhFrame.ModelLayer.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Model.t3
{
    public class t3_json : BaseModel
    {
        public t3_json(bool newInstance) : base(newInstance)
        {


        }

        public t3_json()
        {


        }
        public int id { get; set; }
        public int codeid { get; set; }
        public int houseid { get; set; }
        public string jsonstr { get; set; }
        public DateTime createtime { get; set; }
        public Guid operatorid { get; set; }
    }
}
