using PZhFrame.Data.DataService;
using PZhFrame.Data.Repository.Extension;
using System;
namespace PZhFrame.ModelLayer.BaseModels
{
    public partial class new_resource_json : BaseModel
    {
           public new_resource_json(){


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
           [Head("jsonvalue", "-")]
           public string jsonvalue { get;set;}


    }
}
