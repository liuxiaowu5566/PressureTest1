using PZhFrame.Data.DataService;
using PZhFrame.Data.Repository.Extension;
using PZhFrame.ModelLayer.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoTest.DataAdd
{
    public partial class t8_history :BaseModel
    {
        public t8_history(bool isIns) : base(isIns)
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [ExplicitKey]
        [Head("id", "-")]
        public int id { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Head("houseid", "-")]
        public int? houseid { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Head("codeid", "-")]
        public int? codeid { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Head("value", "-")]
        public string value { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Head("createtime", "-")]
        public DateTime? createtime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Head("opreatorid", "-")]
        public Guid? opreatorid { get; set; }

    }
}
