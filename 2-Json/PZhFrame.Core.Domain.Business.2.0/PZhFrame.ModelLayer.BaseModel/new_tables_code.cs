using PZhFrame.Data.DataService;
using PZhFrame.Data.Repository.Extension;
using System;

namespace PZhFrame.ModelLayer.BaseModels
{
    public partial class new_tables_code:BaseModel
    {
           public new_tables_code(){


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
           [Head("name","-")]
           public string name {get;set;}

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
