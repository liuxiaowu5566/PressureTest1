using System;
using System.Linq;
using System.Text;
using PZhFrame.Data.DataService;
using PZhFrame.Data.Repository.Extension;
namespace PZhFrame.ModelLayer.BaseModels
{
    public partial class new_resource_json_info : BaseModel
    {
        public new_resource_json_info()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [ExplicitKey]
        [Head("id", "-")]
        public Int64 id { get; set; }


        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Head("jsonvalue", "-")]
        public string jsonvalue { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        //[Head("house_code", "-")]
        //public string house_code { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Head("house_estate_code", "-")]
        public string house_estate_code { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        //[Head("modifytime", "-")]
        //public string modifytime { get; set; }
    }
}
