using System;
using System.Linq;
using System.Text;
using PZhFrame.Data.DataService;
using PZhFrame.Data.Repository.Extension;
namespace PZhFrame.ModelLayer.BaseModels
{
    public partial class new_fileds_code : BaseModel
    {
           public new_fileds_code(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
	   [ExplicitKey]    
           [Head("id","-")]
           public Guid id {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [Head("table_id","-")]
           public Guid table_id {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Head("table_name", "-")]
        public string table_name { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Head("name","-")]
           public string name {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Head("utype", "-")]
        public string utype { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Head("isdelete","-")]
           public bool? isdelete {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("accountid","-")]
           public Guid? accountid {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("createtime","-")]
           public DateTime? createtime {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("modifytime","-")]
           public DateTime? modifytime {get;set;}

    }
}
