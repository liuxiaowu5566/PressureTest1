using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Model
{
    public class ModifyLogModel
    {
        public string value { get; set; }
        public Guid accountid { get; set; }
        public DateTime modifytime {get;set;}
    }
}
