﻿using System;
using System.Linq;
using System.Text;
using PZhFrame.Data.DataService;
using PZhFrame.Data.Repository.Extension;
using PZhFrame.ModelLayer.BaseModels;

namespace Models.Model.t4
{
    public partial class t4_code : BaseModel
    {
           public t4_code(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
	   [ExplicitKey]    
           [Head("id","-")]
           public int id {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [Head("name","-")]
           public string name {get;set;}

    }
}
