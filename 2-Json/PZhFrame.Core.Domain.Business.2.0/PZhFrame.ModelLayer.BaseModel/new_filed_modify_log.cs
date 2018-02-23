using System;
using System.Linq;
using System.Text;
using PZhFrame.Data.DataService;
using PZhFrame.Data.Repository.Extension;
namespace PZhFrame.ModelLayer.BaseModels
{
    public partial class new_filed_modify_log : BaseModel
    {
           public new_filed_modify_log(){


           }

        public new_filed_modify_log(bool newInstance) : base(newInstance)
        {


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
           /// Nullable:True
           /// </summary>           
           [Head("resource_id","-")]
           public Guid? resource_id {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("filed_id","-")]
           public Guid? filed_id {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("old_id","-")]
           public Guid? old_id {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("value","-")]
           public string value {get;set;}

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
           [Head("is_delete","-")]
           public bool? is_delete {get;set;}

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
