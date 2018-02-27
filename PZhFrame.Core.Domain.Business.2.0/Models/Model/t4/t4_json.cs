using System;
using System.Linq;
using System.Text;
using PZhFrame.Data.DataService;
using PZhFrame.Data.Repository.Extension;
using PZhFrame.ModelLayer.BaseModels;

namespace Models.Model.t4
{
    public partial class t4_json : BaseModel
    {

        public t4_json(bool newInstance) : base(newInstance)
        {


        }

        public t4_json()
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
        [Head("jsonstr", "-")]
        public string jsonstr { get; set; }

    }
}
