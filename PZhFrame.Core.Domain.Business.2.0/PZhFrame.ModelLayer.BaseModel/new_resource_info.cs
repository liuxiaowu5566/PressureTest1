using PZhFrame.Data.DataService;
using PZhFrame.Data.Repository.Extension;
using System;
namespace PZhFrame.ModelLayer.BaseModels
{
    public partial class new_resource_info : BaseModel
    {
           public new_resource_info(){


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
           [Head("table_id","-")]
           public Guid? table_id {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("merge_ids","-")]
           public string merge_ids {get;set;}

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
           [Head("isdelete","-")]
           public bool? isdelete {get;set;}

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
