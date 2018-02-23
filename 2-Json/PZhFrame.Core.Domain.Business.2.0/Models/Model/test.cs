using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PZhFrame.Data.DataService;
using PZhFrame.Data.Repository.Extension;
using PZhFrame.ModelLayer.BaseModels;

namespace Models.Model
{
    public partial class test:BaseModel
    {
           public test(){


           }
           /// <summary>
           /// Desc:主键ID
           /// Default:
           /// Nullable:False
           /// </summary>           
	   [ExplicitKey]    
           [Head("id","主键ID")]
           public Int64 id {get;set;}

           
           [Head("name","-")]
           public string name {get;set;}

    }
}
